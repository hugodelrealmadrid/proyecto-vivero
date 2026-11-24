import { createContext, useContext, useEffect, useState } from "react";
import { request } from "./api.js";
const Context = createContext(null);
export const useAuth = () => useContext(Context);
export function AuthProvider({ children }) {
  const [user, setUser] = useState(null),
    [loading, setLoading] = useState(true),
    [error, setError] = useState("");
  async function refresh() {
    setLoading(true);
    setError("");
    try {
      const data = await request("/api/auth/me");
      setUser(data.user);
    } catch (err) {
      setUser(null);
      setError(err.message);
    } finally {
      setLoading(false);
    }
  }
  useEffect(() => {
    refresh();
    const sync = () => {
      if (document.visibilityState === "visible") refresh();
    };
    document.addEventListener("visibilitychange", sync);
    return () => document.removeEventListener("visibilitychange", sync);
  }, []);
  async function authenticate(values, register) {
    const { user } = await request(
      `/api/auth/${register ? "register" : "login"}`,
      { method: "POST", body: JSON.stringify(values) },
    );
    setUser(user);
    return user;
  }
  async function logout() {
    await request("/api/auth/logout", { method: "POST", body: "{}" });
    setUser(null);
    window.location.hash = "inicio";
  }
  return (
    <Context.Provider
      value={{ user, loading, error, refresh, authenticate, logout }}
    >
      {children}
    </Context.Provider>
  );
}
