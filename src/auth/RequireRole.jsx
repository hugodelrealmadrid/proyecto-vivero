import { useEffect } from "react";
import { useAuth } from "./AuthContext.jsx";
export default function RequireRole({ role, children }) {
  const { user, loading, error, refresh } = useAuth();
  useEffect(() => {
    if (!loading && !user && !error) window.location.replace("#iniciar-sesion");
  }, [loading, user, error]);
  if (loading)
    return (
      <p className="notice" role="status">
        Comprobando sesión…
      </p>
    );
  if (error)
    return (
      <section className="section-panel">
        <h1>No se pudo comprobar la sesión.</h1>
        <p className="notice">{error}</p>
        <button className="button" onClick={refresh}>
          Reintentar
        </button>
      </section>
    );
  if (!user)
    return (
      <p className="notice" role="status">
        Abriendo inicio de sesión…
      </p>
    );
  if (user.role !== role)
    return (
      <section className="section-panel">
        <h1>Acceso restringido</h1>
        <p className="notice">
          {role === "admin"
            ? "Esta página es exclusiva del administrador."
            : "Esta página es exclusiva de las cuentas de usuario."}
        </p>
        <a
          className="button primary"
          href={user.role === "admin" ? "#admin" : "#usuarios"}
        >
          Volver a mi espacio
        </a>
      </section>
    );
  return children;
}
