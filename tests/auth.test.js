import { test, before, after } from "node:test";
import assert from "node:assert/strict";
import { createServer } from "node:http";
import { mkdtempSync, readFileSync, rmSync } from "node:fs";
import { tmpdir } from "node:os";
import { join } from "node:path";
import { createAuthApi } from "../server/auth.js";

let server, base, adminCookie, userCookie;
const dir = mkdtempSync(join(tmpdir(), "mapache-auth-test-"));
async function call(
  path,
  { body, cookie, origin, method = body ? "POST" : "GET" } = {},
) {
  const response = await fetch(base + path, {
    method,
    headers: {
      ...(body ? { "Content-Type": "application/json" } : {}),
      ...(cookie ? { Cookie: cookie } : {}),
      ...(origin ? { Origin: origin } : {}),
    },
    ...(body ? { body: JSON.stringify(body) } : {}),
  });
  return {
    status: response.status,
    data: await response.json(),
    cookie: response.headers.get("set-cookie")?.split(";")[0],
    headers: response.headers,
  };
}
before(async () => {
  server = createServer(createAuthApi({ dataDir: dir }));
  await new Promise((resolve) => server.listen(0, "127.0.0.1", resolve));
  base = `http://127.0.0.1:${server.address().port}`;
});
after(async () => {
  await new Promise((resolve) => server.close(resolve));
  rmSync(dir, { recursive: true, force: true });
});
test("un visitante no tiene sesión ni acceso a datos privados", async () => {
  assert.equal((await call("/api/auth/me")).data.user, null);
  assert.equal((await call("/api/admin/users")).status, 401);
  assert.equal((await call("/api/user/profile")).status, 401);
});
test("las cuentas predeterminadas tienen contraseñas hash y datos públicos sin secretos", async () => {
  const saved = readFileSync(join(dir, "accounts.json"), "utf8");
  assert.ok(!saved.includes("admin12345"));
  assert.ok(!saved.includes("usuario12345"));
  const admin = await call("/api/auth/login", {
    body: { email: "ADMIN@gmail.com", password: "admin12345" },
  });
  assert.equal(admin.status, 200);
  assert.equal(admin.data.user.role, "admin");
  assert.equal(admin.data.user.hash, undefined);
  assert.equal(admin.data.user.salt, undefined);
  assert.match(admin.headers.get("set-cookie"), /HttpOnly/);
  assert.match(admin.headers.get("set-cookie"), /SameSite=Strict/);
  adminCookie = admin.cookie;
  assert.equal(
    (await call("/api/admin/users", { cookie: adminCookie })).data.users.length,
    2,
  );
});
test("la cuenta de usuario no puede entrar a la API de administración", async () => {
  const user = await call("/api/auth/login", {
    body: { email: "usuario@gmail.com", password: "usuario12345" },
  });
  assert.equal(user.status, 200);
  assert.equal(user.data.user.role, "user");
  userCookie = user.cookie;
  assert.equal(
    (await call("/api/admin/users", { cookie: userCookie })).status,
    403,
  );
  assert.equal(
    (await call("/api/user/profile", { cookie: userCookie })).data.user.email,
    "usuario@gmail.com",
  );
  assert.equal(
    (await call("/api/user/profile", { cookie: adminCookie })).status,
    403,
  );
});
test("rechaza contraseñas incorrectas y entradas inválidas", async () => {
  assert.equal(
    (
      await call("/api/auth/login", {
        body: { email: "admin@gmail.com", password: "incorrecta" },
      })
    ).status,
    401,
  );
  assert.equal(
    (
      await call("/api/auth/register", {
        body: { name: "", email: "bad", password: "123" },
      })
    ).status,
    400,
  );
  assert.equal((await call("/api/auth/login", { body: [] })).status, 400);
});
test("el registro crea un usuario persistente sin permitir elevar su rol", async () => {
  const data = {
    name: "Cuenta de prueba",
    email: "prueba@example.test",
    password: "prueba12345",
    role: "admin",
  };
  const registration = await call("/api/auth/register", { body: data });
  assert.equal(registration.status, 201);
  assert.equal(registration.data.user.role, "user");
  assert.equal(
    (await call("/api/auth/me", { cookie: registration.cookie })).data.user
      .name,
    "Cuenta de prueba",
  );
  assert.equal(
    (await call("/api/admin/users", { cookie: registration.cookie })).status,
    403,
  );
  assert.equal(
    (
      await call("/api/auth/register", {
        body: { ...data, email: "PRUEBA@example.test" },
      })
    ).status,
    409,
  );
  assert.ok(
    !readFileSync(join(dir, "accounts.json"), "utf8").includes("prueba12345"),
  );
  const second = createServer(createAuthApi({ dataDir: dir }));
  await new Promise((resolve) => second.listen(0, "127.0.0.1", resolve));
  try {
    const result = await fetch(
      `http://127.0.0.1:${second.address().port}/api/auth/login`,
      {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data),
      },
    );
    assert.equal(result.status, 200);
  } finally {
    await new Promise((resolve) => second.close(resolve));
  }
});
test("rechaza peticiones de otro origen", async () => {
  assert.equal(
    (
      await call("/api/auth/login", {
        origin: "https://other.example",
        body: { email: "admin@gmail.com", password: "admin12345" },
      })
    ).status,
    403,
  );
});
test("cerrar sesión invalida la cookie en el servidor", async () => {
  assert.equal(
    (await call("/api/auth/logout", { cookie: userCookie, body: {} })).status,
    200,
  );
  assert.equal(
    (await call("/api/auth/me", { cookie: userCookie })).data.user,
    null,
  );
  assert.equal(
    (await call("/api/user/profile", { cookie: userCookie })).status,
    401,
  );
});
