import EmptyTable from "../components/EmptyTable.jsx";
import Icon from "../components/Icon.jsx";
import { useAuth } from "../auth/AuthContext.jsx";
const userSections = [
  {
    key: "plantas",
    name: "Plantas",
    icon: "plant",
    fields: ["Planta", "Especie", "Descripción", "Precio de venta"],
  },
  {
    key: "pedidos",
    name: "Mis pedidos",
    icon: "cart",
    fields: ["Pedido", "Fecha", "Estado", "Total"],
  },
  {
    key: "entregas",
    name: "Mis entregas",
    icon: "truck",
    fields: ["Entrega", "Pedido", "Dirección", "Estado", "Fecha programada"],
  },
  {
    key: "proyectos",
    name: "Mis proyectos",
    icon: "tools",
    fields: [
      "Proyecto",
      "Ubicación",
      "Estado",
      "Fecha de inicio",
      "Fecha de fin",
    ],
  },
  {
    key: "perfil",
    name: "Mi perfil",
    icon: "user",
    fields: ["Nombre", "Teléfono", "Correo", "Dirección"],
  },
];
export default function UserPage({ sectionKey }) {
  const { user } = useAuth();
  const section = userSections.find((s) => s.key === sectionKey);
  if (sectionKey && !section)
    return (
      <>
        <h1>Sección no encontrada</h1>
        <a className="button" href="#usuarios">
          Volver a mi espacio
        </a>
      </>
    );
  return (
    <>
      <nav className="breadcrumbs" aria-label="Ruta de navegación">
        <a href="#inicio">Principal</a>
        <span>/</span>
        <a href="#usuarios">Usuarios</a>
        {section && (
          <>
            <span>/</span>
            <span aria-current="page">{section.name}</span>
          </>
        )}
      </nav>
      <section className="hero">
        <div>
          <p className="eyebrow">PORTAL DE USUARIOS</p>
          <h1>{section ? section.name : `Bienvenido, ${user.name}.`}</h1>
          <p>
            {section
              ? "Toda la información de tu jardín, en un solo lugar."
              : "Consulta plantas, pedidos, entregas y proyectos desde tu espacio personal."}
          </p>
        </div>
      </section>
      <nav className="page-tabs" aria-label="Secciones de usuario">
        <a href="#usuarios" className={!section ? "active" : ""}>
          Resumen
        </a>
        {userSections.map((s) => (
          <a
            key={s.key}
            href={`#usuarios/${s.key}`}
            className={section?.key === s.key ? "active" : ""}
            aria-current={section?.key === s.key ? "page" : undefined}
          >
            {s.name}
          </a>
        ))}
      </nav>
      {section ? (
        <section className="section-panel">
          <div className="panel-heading">
            <h2>{section.name}</h2>
            <span className="record-count">Sin datos</span>
          </div>
          {section.key === "perfil" ? (
            <dl className="profile-details">
              <div>
                <dt>Nombre</dt>
                <dd>{user.name}</dd>
              </div>
              <div>
                <dt>Correo electrónico</dt>
                <dd>{user.email}</dd>
              </div>
              <div>
                <dt>Tipo de cuenta</dt>
                <dd>Usuario</dd>
              </div>
              <div>
                <dt>Teléfono</dt>
                <dd>Sin registrar</dd>
              </div>
              <div>
                <dt>Dirección</dt>
                <dd>Sin registrar</dd>
              </div>
            </dl>
          ) : (
            <EmptyTable
              fields={section.fields}
              description={
                section.key === "perfil"
                  ? "Tu información aparecerá al iniciar sesión."
                  : "Aún no hay información disponible en esta sección."
              }
            />
          )}
        </section>
      ) : (
        <>
          <section className="module-grid user-grid">
            {userSections.map((s) => (
              <article className="module-card" key={s.key}>
                <div className="card-title">
                  <Icon name={s.icon} />
                  <h2>{s.name}</h2>
                </div>
                <p className="notice">Sin registros</p>
                <a className="button" href={`#usuarios/${s.key}`}>
                  Abrir sección
                </a>
              </article>
            ))}
          </section>
          <section className="section-panel">
            <div className="panel-heading">
              <h2>Mis pedidos recientes</h2>
              <a href="#usuarios/pedidos" className="button">
                Ver pedidos
              </a>
            </div>
            <EmptyTable fields={["Pedido", "Fecha", "Estado", "Total"]} />
          </section>
        </>
      )}
    </>
  );
}
