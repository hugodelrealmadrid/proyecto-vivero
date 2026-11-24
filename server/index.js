import { createServer } from "node:http";
import { readFile, stat } from "node:fs/promises";
import { resolve, extname, sep } from "node:path";
import { createAuthApi } from "./auth.js";
const root = resolve("dist");
const mime = {
  ".html": "text/html; charset=utf-8",
  ".js": "text/javascript; charset=utf-8",
  ".css": "text/css; charset=utf-8",
  ".png": "image/png",
  ".jpg": "image/jpeg",
  ".svg": "image/svg+xml",
  ".json": "application/json",
};
const api = createAuthApi();
const server = createServer((req, res) =>
  api(req, res, async () => {
    try {
      if (!["GET", "HEAD"].includes(req.method)) {
        res.writeHead(405);
        return res.end();
      }
      const path = decodeURIComponent(
        new URL(req.url, "http://localhost").pathname,
      );
      const file = resolve(root, "." + (path === "/" ? "/index.html" : path));
      if (!file.startsWith(root + sep) || !(await stat(file)).isFile()) {
        res.writeHead(404);
        return res.end("No encontrado");
      }
      const data = await readFile(file);
      res.writeHead(200, {
        "Content-Type": mime[extname(file)] || "application/octet-stream",
        "X-Content-Type-Options": "nosniff",
        "Cache-Control": "no-cache",
      });
      res.end(req.method === "HEAD" ? undefined : data);
    } catch {
      res.writeHead(404);
      res.end("No encontrado");
    }
  }),
);
const port = Number(process.env.PORT || 5184);
server.listen(port, process.env.HOST || "127.0.0.1", () =>
  console.log(`El Jardín del Mapache: http://127.0.0.1:${port}`),
);
