import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import { createAuthApi } from "./server/auth.js";
export default defineConfig({
  plugins: [
    react(),
    {
      name: "mapache-auth-api",
      configureServer(server) {
        server.middlewares.use(createAuthApi());
      },
      configurePreviewServer(server) {
        server.middlewares.use(createAuthApi());
      },
    },
  ],
  base: "./",
  server: {
    host: "127.0.0.1",
    port: 5184,
    fs: {
      deny: [
        ".env",
        ".env.*",
        "*.{crt,pem}",
        "**/.git/**",
        "**/.data/**",
        "**/server/**",
      ],
    },
  },
});
