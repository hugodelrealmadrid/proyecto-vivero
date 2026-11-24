import { useEffect, useRef, useState } from "react";
import Icon from "./Icon.jsx";
import { modules } from "../modules.js";
import { useAuth } from "../auth/AuthContext.jsx";
const normalize = (value) =>
  value
    .normalize("NFD")
    .replace(/[\u0300-\u036f]/g, "")
    .toLowerCase();
export default function Layout({ route, onDialog, children }) {
  const { user, logout } = useAuth();
  const [logoutError, setLogoutError] = useState("");
  const [query, setQuery] = useState("");
  const [open, setOpen] = useState(null);
  const [showSearch, setShowSearch] = useState(false);
  const input = useRef(null),
    header = useRef(null),
    main = useRef(null);
  useEffect(() => {
    const keydown = (e) => {
      if (
        e.key === "/" &&
        !["INPUT", "TEXTAREA", "SELECT"].includes(
          document.activeElement.tagName,
        )
      ) {
        e.preventDefault();
        input.current.focus();
      }
      if (e.key === "Escape") {
        setOpen(null);
        setShowSearch(false);
      }
    };
    const outside = (e) => {
      if (!header.current?.contains(e.target)) {
        setOpen(null);
        setShowSearch(false);
      }
    };
    document.addEventListener("keydown", keydown);
    document.addEventListener("pointerdown", outside);
    return () => {
      document.removeEventListener("keydown", keydown);
      document.removeEventListener("pointerdown", outside);
    };
  }, []);
  useEffect(() => {
    setOpen(null);
    setShowSearch(false);
    setQuery("");
    main.current?.focus();
    window.scrollTo(0, 0);
  }, [route]);
  const results = (user?.role === "admin" ? modules : [])
    .flatMap((g) => [
      { name: g.name, group: "Módulo", href: `#${g.key}` },
      ...g.entities.map((e) => ({
        name: e.name,
        group: g.name,
        href: `#${g.key}/${e.key}`,
      })),
    ])
    .concat(
      user
        ? [
            {
              name: "Notificaciones",
              group: "Sistema",
              href: "#notificaciones",
            },
          ]
        : [],
    )
    .concat(
      user?.role === "user"
        ? [
            {
              name: "Mis pedidos",
              group: "Mi jardín",
              href: "#usuarios/pedidos",
            },
            { name: "Plantas", group: "Mi jardín", href: "#usuarios/plantas" },
            {
              name: "Mis entregas",
              group: "Mi jardín",
              href: "#usuarios/entregas",
            },
            {
              name: "Mis proyectos",
              group: "Mi jardín",
              href: "#usuarios/proyectos",
            },
            { name: "Mi perfil", group: "Mi jardín", href: "#usuarios/perfil" },
          ]
        : [],
    )
    .concat(
      !user
        ? [
            {
              name: "Iniciar sesión",
              group: "Acceso",
              href: "#iniciar-sesion",
            },
            { name: "Crear cuenta", group: "Registro", href: "#registro" },
          ]
        : [],
    )
    .filter((x) => normalize(x.name + " " + x.group).includes(normalize(query)))
    .slice(0, 12);
  return (
    <>
      <a
        href="#contenido"
        className="skip-link"
        onClick={(e) => {
          e.preventDefault();
          main.current.focus();
        }}
      >
        Saltar al contenido
      </a>
      <div className="utility">
        <span>
          El Jardín del Mapache
          {user?.role === "admin"
            ? " · Administración"
            : user
              ? " · Mi jardín"
              : ""}
        </span>
        <span>Tu jardín, en un solo lugar</span>
      </div>
      <div ref={header}>
        <header className="header">
          <a
            className="brand"
            href="#inicio"
            aria-label="El Jardín del Mapache, inicio"
          >
            <span className="brand-mark">
              <img src={`${import.meta.env.BASE_URL}logo.png`} alt="" />
            </span>
            <span>
              <strong>
                El Jardín
                <br />
                del Mapache
              </strong>
              <small>
                {user?.role === "admin"
                  ? "GESTIÓN DEL VIVERO"
                  : "NATURALEZA CON PERSONALIDAD"}
              </small>
            </span>
          </a>
          <div className="search-wrap">
            <label className="search">
              <Icon name="search" />
              <input
                ref={input}
                value={query}
                onChange={(e) => {
                  setQuery(e.target.value);
                  setShowSearch(true);
                }}
                onFocus={() => setShowSearch(true)}
                onKeyDown={(e) => {
                  if (e.key === "Enter" && query.trim() && results.length) {
                    window.location.hash = results[0].href;
                    setShowSearch(false);
                  }
                }}
                type="search"
                placeholder="Buscar plantas, pedidos, tareas o módulos…"
                aria-label="Buscar módulos"
                aria-controls="search-results"
                aria-expanded={showSearch && !!query.trim()}
                autoComplete="off"
              />
              <kbd>/</kbd>
            </label>
            {showSearch && !!query.trim() && (
              <div id="search-results" className="search-results">
                <p className="result-count" role="status">
                  {results.length
                    ? `${results.length} resultados`
                    : "No hay módulos que coincidan."}
                </p>
                {results.map((x) => (
                  <a
                    key={x.href}
                    href={x.href}
                    onClick={() => setShowSearch(false)}
                  >
                    {x.name}
                    <small>{x.group}</small>
                  </a>
                ))}
              </div>
            )}
          </div>
          <div className="header-actions">
            <button
              onClick={() => onDialog("help")}
              className="text-button"
              aria-label="Ayuda"
            >
              <Icon name="help" />
              <span>Ayuda</span>
            </button>
            <a
              href={
                user
                  ? user.role === "admin"
                    ? "#admin"
                    : "#usuarios"
                  : "#iniciar-sesion"
              }
              className="text-button"
              aria-label={user ? "Mi cuenta" : "Iniciar sesión"}
            >
              <Icon name="user" />
              <span>{user ? user.name : "Iniciar sesión"}</span>
            </a>
            {user && (
              <a
                href="#notificaciones"
                className="icon-button"
                aria-label="Notificaciones"
              >
                <Icon name="bell" />
              </a>
            )}
            {user && (
              <button
                className="text-button"
                onClick={async () => {
                  setLogoutError("");
                  try {
                    await logout();
                  } catch (err) {
                    setLogoutError(err.message);
                  }
                }}
              >
                Salir
              </button>
            )}
          </div>
        </header>
        <nav className="portal-navigation" aria-label="Páginas principales">
          <a
            href="#inicio"
            className={route === "inicio" ? "active" : ""}
            aria-current={route === "inicio" ? "page" : undefined}
          >
            Principal
          </a>
          {user?.role === "user" && (
            <a
              href="#usuarios"
              className={route.split("/")[0] === "usuarios" ? "active" : ""}
              aria-current={route === "usuarios" ? "page" : undefined}
            >
              Usuarios
            </a>
          )}
          {user?.role === "admin" && (
            <a
              href="#admin"
              className={
                ["admin", ...modules.map((g) => g.key)].includes(
                  route.split("/")[0],
                )
                  ? "active"
                  : ""
              }
              aria-current={route === "admin" ? "page" : undefined}
            >
              Admin
            </a>
          )}
          {!user && (
            <>
              <a
                href="#iniciar-sesion"
                className={route === "iniciar-sesion" ? "active" : ""}
              >
                Iniciar sesión
              </a>
              <a
                href="#registro"
                className={route === "registro" ? "active" : ""}
              >
                Crear cuenta
              </a>
            </>
          )}
        </nav>
        {user?.role === "admin" &&
          (route === "admin" ||
            modules.some((g) => g.key === route.split("/")[0])) && (
            <nav className="navigation" aria-label="Navegación principal">
              <a
                href="#admin"
                className={`nav-link home-link ${route === "admin" ? "active" : ""}`}
                aria-current={route === "admin" ? "page" : undefined}
              >
                <Icon name="home" />
                Panel
              </a>
              {modules.map((g) => (
                <div className="nav-group" key={g.key}>
                  <div
                    className={`nav-item ${route.split("/")[0] === g.key ? "active" : ""}`}
                  >
                    <a
                      href={`#${g.key}`}
                      className="nav-link"
                      aria-current={route === g.key ? "page" : undefined}
                    >
                      <Icon name={g.icon} />
                      {g.name}
                    </a>
                    <button
                      className="dropdown-toggle"
                      onClick={() => setOpen(open === g.key ? null : g.key)}
                      aria-expanded={open === g.key}
                      aria-controls={`nav-${g.key}`}
                      aria-label={`Submenú de ${g.name}`}
                    >
                      <Icon name="chevron" />
                    </button>
                  </div>
                  {open === g.key && (
                    <div className="subnav" id={`nav-${g.key}`}>
                      {g.entities.map((e) => (
                        <a key={e.key} href={`#${g.key}/${e.key}`}>
                          {e.name}
                        </a>
                      ))}
                    </div>
                  )}
                </div>
              ))}
            </nav>
          )}
      </div>
      <main ref={main} id="contenido" tabIndex={-1}>
        {logoutError && (
          <p role="alert" className="form-message error">
            {logoutError}
          </p>
        )}
        {children}
      </main>
      <footer>
        <span>El Jardín del Mapache</span>
        <span>Plantas, personas y proyectos que crecen juntos.</span>
        {user?.role === "admin" && <a href="#logos">Identidad visual</a>}
      </footer>
    </>
  );
}
