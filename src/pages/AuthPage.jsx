import { useState } from "react";
import Icon from "../components/Icon.jsx";
import { useAuth } from "../auth/AuthContext.jsx";

export default function AuthPage({ register = false }) {
  const [visible, setVisible] = useState(false);
  const [message, setMessage] = useState("");
  const [error, setError] = useState("");
  const [busy, setBusy] = useState(false);
  const { authenticate, user } = useAuth();
  async function submit(event) {
    event.preventDefault();
    setMessage("");
    setError("");
    const form = event.currentTarget;
    if (!form.reportValidity()) return;
    const values = new FormData(form);
    if (register && !String(values.get("name")).trim()) {
      setError("Escribe tu nombre completo.");
      return;
    }
    if (register && values.get("password") !== values.get("confirmation")) {
      setError("Las contraseñas no coinciden.");
      return;
    }
    setBusy(true);
    try {
      const account = await authenticate(
        {
          name: values.get("name"),
          email: values.get("email"),
          password: values.get("password"),
        },
        register,
      );
      form.reset();
      window.location.hash = account.role === "admin" ? "admin" : "usuarios";
    } catch (err) {
      setError(err.message);
    } finally {
      setBusy(false);
    }
  }
  if (user)
    return (
      <section className="hero">
        <div>
          <h1>Ya tienes una sesión iniciada.</h1>
          <p>Continúa a tu espacio personal.</p>
          <a
            className="button primary"
            href={user.role === "admin" ? "#admin" : "#usuarios"}
            style={{ marginTop: 20 }}
          >
            Ir a mi espacio
          </a>
        </div>
      </section>
    );
  return (
    <section className="auth-page">
      <aside className="auth-story">
        <a href="#inicio" className="back-link">
          Volver al jardín
        </a>
        <img
          src={`${import.meta.env.BASE_URL}logo.png`}
          alt="Mapache entre hojas"
        />
        <p className="eyebrow">EL JARDÍN DEL MAPACHE</p>
        <h1>
          {register
            ? "Dale espacio a lo que viene."
            : "Qué bueno verte de nuevo."}
        </h1>
        <p>
          {register
            ? "Crea tu espacio para acompañar cada planta, pedido y proyecto."
            : "Tu jardín, tus pedidos y tus proyectos te esperan en un solo lugar."}
        </p>
      </aside>
      <div className="auth-card">
        <div className="auth-tabs">
          <a href="#iniciar-sesion" className={!register ? "active" : ""}>
            Iniciar sesión
          </a>
          <a href="#registro" className={register ? "active" : ""}>
            Crear cuenta
          </a>
        </div>
        <p className="eyebrow">
          {register ? "EMPIEZA A CRECER" : "BIENVENIDO A TU JARDÍN"}
        </p>
        <h2>{register ? "Crea tu cuenta" : "Inicia sesión"}</h2>
        <p className="auth-description">
          {register
            ? "Completa tus datos para crear tu cuenta."
            : "Ingresa tu correo y contraseña."}
        </p>
        <form onSubmit={submit}>
          {register && (
            <label className="form-field">
              Nombre completo
              <input
                name="name"
                autoComplete="name"
                placeholder="Tu nombre completo"
                required
                maxLength={100}
              />
            </label>
          )}
          <label className="form-field">
            Correo electrónico
            <input
              name="email"
              type="email"
              autoComplete="email"
              placeholder="nombre@correo.com"
              required
              maxLength={254}
            />
          </label>
          <label className="form-field" htmlFor="password">
            Contraseña
          </label>
          <div className="password-field">
            <input
              id="password"
              name="password"
              type={visible ? "text" : "password"}
              autoComplete={register ? "new-password" : "current-password"}
              placeholder={register ? "Crea una contraseña" : "Tu contraseña"}
              required
              minLength={register ? 8 : undefined}
              maxLength={128}
              aria-describedby={register ? "password-hint" : undefined}
            />
            <button
              type="button"
              onClick={() => setVisible(!visible)}
              aria-label={visible ? "Ocultar contraseña" : "Mostrar contraseña"}
              aria-pressed={visible}
            >
              {visible ? "Ocultar" : "Mostrar"}
            </button>
          </div>
          {register && (
            <>
              <p id="password-hint" className="field-hint">
                Usa al menos 8 caracteres.
              </p>
              <label className="form-field">
                Confirmar contraseña
                <input
                  name="confirmation"
                  type={visible ? "text" : "password"}
                  autoComplete="new-password"
                  placeholder="Repite tu contraseña"
                  required
                  minLength={8}
                  maxLength={128}
                />
              </label>
            </>
          )}
          {error && (
            <p className="form-message error" role="alert">
              {error}
            </p>
          )}
          {message && (
            <p className="form-message" role="status">
              {message}
            </p>
          )}
          <button
            className="button primary auth-submit"
            type="submit"
            disabled={busy}
          >
            {busy
              ? "Procesando…"
              : register
                ? "Crear cuenta"
                : "Iniciar sesión"}
          </button>
        </form>
        <p className="auth-switch">
          {register ? "¿Ya tienes una cuenta?" : "¿Es tu primera visita?"}{" "}
          <a href={register ? "#iniciar-sesion" : "#registro"}>
            {register ? "Inicia sesión" : "Crea tu cuenta"}
          </a>
        </p>
        <div className="auth-footnote">
          <Icon name="plant" />
          <span>Un espacio para que tu jardín siga creciendo.</span>
        </div>
      </div>
    </section>
  );
}
