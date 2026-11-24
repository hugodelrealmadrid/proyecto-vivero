import {
  randomBytes,
  randomUUID,
  scrypt,
  scryptSync,
  timingSafeEqual,
} from "node:crypto";
import { promisify } from "node:util";
import {
  mkdirSync,
  readFileSync,
  writeFileSync,
  renameSync,
  existsSync,
} from "node:fs";
import { resolve } from "node:path";

const derive = promisify(scrypt);
const lifetime = 8 * 60 * 60 * 1000;
const cookieName = "mapache_session";
const publicUser = ({ id, name, email, role, createdAt }) => ({
  id,
  name,
  email,
  role,
  createdAt,
});
const emailIsValid = (email) =>
  typeof email === "string" &&
  email.length <= 254 &&
  /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);
const passwordIsValid = (password) =>
  typeof password === "string" &&
  password.length >= 8 &&
  password.length <= 128;

export function createAuthApi({
  dataDir = resolve(".data"),
  seed = true,
} = {}) {
  mkdirSync(dataDir, { recursive: true });
  const file = resolve(dataDir, "accounts.json");
  let accounts = existsSync(file) ? JSON.parse(readFileSync(file, "utf8")) : [];
  const sessions = new Map(),
    attempts = new Map();
  const save = () => {
    writeFileSync(file + ".tmp", JSON.stringify(accounts, null, 2), {
      mode: 0o600,
    });
    renameSync(file + ".tmp", file);
  };
  if (seed) {
    const initial = [
      {
        name: "Administrador",
        email: process.env.ADMIN_EMAIL || "admin@gmail.com",
        password: process.env.ADMIN_PASSWORD || "admin12345",
        role: "admin",
      },
      {
        name: "Usuario",
        email: process.env.USER_EMAIL || "usuario@gmail.com",
        password: process.env.USER_PASSWORD || "usuario12345",
        role: "user",
      },
    ];
    let changed = false;
    for (const account of initial) {
      const email = account.email.trim().toLowerCase();
      if (accounts.some((a) => a.email === email)) continue;
      if (!emailIsValid(email) || !passwordIsValid(account.password))
        throw Error("Configuración de cuentas iniciales inválida.");
      const salt = randomBytes(16).toString("hex");
      accounts.push({
        id: randomUUID(),
        name: account.name,
        email,
        role: account.role,
        salt,
        hash: scryptSync(account.password, salt, 64).toString("hex"),
        createdAt: new Date().toISOString(),
      });
      changed = true;
    }
    if (changed) save();
  }
  const dummySalt = randomBytes(16).toString("hex");
  function tokenOf(req) {
    const raw = (req.headers.cookie || "")
      .split(";")
      .map((x) => x.trim())
      .find((x) => x.startsWith(cookieName + "="));
    return raw?.slice(cookieName.length + 1);
  }
  function current(req) {
    const token = tokenOf(req),
      session = sessions.get(token);
    if (!session) return null;
    if (session.expires < Date.now()) {
      sessions.delete(token);
      return null;
    }
    return accounts.find((a) => a.id === session.userId) || null;
  }
  function cookie(res, value, age) {
    res.setHeader(
      "Set-Cookie",
      `${cookieName}=${value}; Path=/; HttpOnly; SameSite=Strict; Max-Age=${age}${process.env.COOKIE_SECURE === "true" ? "; Secure" : ""}`,
    );
  }
  function startSession(req, res, user) {
    sessions.delete(tokenOf(req));
    const token = randomBytes(32).toString("hex");
    sessions.set(token, { userId: user.id, expires: Date.now() + lifetime });
    cookie(res, token, lifetime / 1000);
  }
  function json(res, status, body) {
    res.writeHead(status, {
      "Content-Type": "application/json; charset=utf-8",
      "Cache-Control": "no-store",
      "X-Content-Type-Options": "nosniff",
    });
    res.end(JSON.stringify(body));
  }
  function limit(req) {
    const now = Date.now();
    for (const [key, item] of attempts)
      if (item.until < now) attempts.delete(key);
    for (const [key, item] of sessions)
      if (item.expires < now) sessions.delete(key);
    const key = req.socket.remoteAddress || "local";
    let item = attempts.get(key);
    if (!item) {
      item = { count: 0, until: now + 15 * 60 * 1000 };
      attempts.set(key, item);
    }
    item.count++;
    return item.count <= 30;
  }
  async function body(req) {
    if (!req.headers["content-type"]?.startsWith("application/json"))
      throw Object.assign(Error("Usa contenido JSON."), { status: 415 });
    let text = "";
    for await (const chunk of req) {
      text += chunk;
      if (Buffer.byteLength(text) > 8192)
        throw Object.assign(Error("El formulario es demasiado grande."), {
          status: 413,
        });
    }
    try {
      return JSON.parse(text);
    } catch {
      throw Object.assign(Error("El formulario no es válido."), {
        status: 400,
      });
    }
  }
  return async function api(
    req,
    res,
    next = () => json(res, 404, { error: "No encontrado." }),
  ) {
    const path = (req.url || "").split("?")[0];
    if (!path.startsWith("/api/")) return next();
    try {
      if (req.method === "POST") {
        // All writes require same-origin JSON requests; no cross-origin CORS is enabled.
        const origin = req.headers.origin;
        if (
          req.headers["sec-fetch-site"] === "cross-site" ||
          (origin && new URL(origin).host !== req.headers.host)
        )
          return json(res, 403, { error: "Origen de solicitud no permitido." });
      }
      if (path === "/api/auth/me" && req.method === "GET")
        return json(res, 200, {
          user: current(req) ? publicUser(current(req)) : null,
        });
      if (path === "/api/auth/logout" && req.method === "POST") {
        sessions.delete(tokenOf(req));
        cookie(res, "", 0);
        return json(res, 200, { ok: true });
      }
      if (
        (path === "/api/auth/login" || path === "/api/auth/register") &&
        req.method === "POST"
      ) {
        if (!limit(req))
          return json(res, 429, {
            error: "Demasiados intentos. Inténtalo de nuevo en 15 minutos.",
          });
        const input = await body(req);
        if (!input || typeof input !== "object" || Array.isArray(input))
          return json(res, 400, { error: "El formulario no es válido." });
        const email =
          typeof input.email === "string"
            ? input.email.trim().toLowerCase()
            : "";
        if (!emailIsValid(email) || !passwordIsValid(input.password))
          return json(res, 400, {
            error:
              "Introduce un correo válido y una contraseña de entre 8 y 128 caracteres.",
          });
        if (path.endsWith("/register")) {
          const name = typeof input.name === "string" ? input.name.trim() : "";
          if (!name || name.length > 100)
            return json(res, 400, {
              error: "Introduce un nombre de hasta 100 caracteres.",
            });
          const salt = randomBytes(16).toString("hex");
          const hash = (await derive(input.password, salt, 64)).toString("hex");
          if (accounts.some((a) => a.email === email))
            return json(res, 409, {
              error: "Ya existe una cuenta con ese correo.",
            });
          // Role is always assigned here. A submitted admin role is ignored.
          const user = {
            id: randomUUID(),
            name,
            email,
            role: "user",
            salt,
            hash,
            createdAt: new Date().toISOString(),
          };
          accounts.push(user);
          try {
            save();
          } catch (error) {
            accounts = accounts.filter((a) => a.id !== user.id);
            throw error;
          }
          startSession(req, res, user);
          return json(res, 201, { user: publicUser(user) });
        }
        const user = accounts.find((a) => a.email === email);
        const hash = await derive(input.password, user?.salt || dummySalt, 64);
        if (!user || !timingSafeEqual(hash, Buffer.from(user.hash, "hex")))
          return json(res, 401, { error: "Correo o contraseña incorrectos." });
        startSession(req, res, user);
        return json(res, 200, { user: publicUser(user) });
      }
      if (path.startsWith("/api/admin/")) {
        const user = current(req);
        if (!user)
          return json(res, 401, { error: "Inicia sesión para continuar." });
        if (user.role !== "admin")
          return json(res, 403, {
            error: "Esta sección es exclusiva del administrador.",
          });
        if (path === "/api/admin/users" && req.method === "GET")
          return json(res, 200, { users: accounts.map(publicUser) });
        if (path === "/api/admin/overview" && req.method === "GET")
          return json(res, 200, {
            role: "admin",
            accounts: accounts.length,
            summary: { sales: 0, pendingOrders: 0, plants: 0 },
            sales: [],
            purchases: [],
            stock: [],
            tasks: [],
          });
      }
      if (path === "/api/user/profile" && req.method === "GET") {
        const user = current(req);
        if (!user)
          return json(res, 401, { error: "Inicia sesión para continuar." });
        if (user.role !== "user")
          return json(res, 403, {
            error: "Esta sección es exclusiva de los usuarios.",
          });
        return json(res, 200, { user: publicUser(user) });
      }
      return json(res, 404, { error: "Servicio no encontrado." });
    } catch (error) {
      json(res, error.status || 500, {
        error: error.status
          ? error.message
          : "No se pudo completar la operación. Inténtalo de nuevo.",
      });
    }
  };
}
