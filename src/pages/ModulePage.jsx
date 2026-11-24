import Icon from "../components/Icon.jsx";
import EmptyTable from "../components/EmptyTable.jsx";
import { moduleComponents } from "../modules/registry.jsx";
export default function ModulePage({ group, entity }) {
  const CustomModule = entity && moduleComponents[entity.key];
  return (
    <>
      <nav className="breadcrumbs" aria-label="Ruta de navegación">
        <a href="#admin">Administración</a>
        <span>/</span>
        {entity ? (
          <>
            <a href={`#${group.key}`}>{group.name}</a>
            <span>/</span>
            <span aria-current="page">{entity.name}</span>
          </>
        ) : (
          <span aria-current="page">{group.name}</span>
        )}
      </nav>
      <div className="page-heading">
        <div>
          <div className="heading-icon">
            <Icon name={group.icon} />
          </div>
          <p className="eyebrow">
            {entity ? group.name : "GESTIÓN DEL JARDÍN"}
          </p>
          <h1>{entity?.name || group.name}</h1>
          <p>{group.description}</p>
        </div>
        {entity && (
          <a href={`#${group.key}`} className="button">
            Ver módulo
          </a>
        )}
      </div>
      {entity ? (
        <>
          <nav className="page-tabs" aria-label="Secciones del módulo">
            {group.entities.map((e) => (
              <a
                className={e.key === entity.key ? "active" : ""}
                aria-current={e.key === entity.key ? "page" : undefined}
                key={e.key}
                href={`#${group.key}/${e.key}`}
              >
                {e.name}
              </a>
            ))}
          </nav>
          {CustomModule ? (
            <CustomModule group={group} entity={entity} />
          ) : (
            <section className="section-panel">
              <div className="panel-heading">
                <div>
                  <h2>Listado de {entity.name.toLowerCase()}</h2>
                  <p>Consulta los registros de esta sección.</p>
                </div>
                <span className="record-count">
                  <strong>0</strong> registros
                </span>
              </div>
              <EmptyTable
                fields={entity.fields.filter((f) => f !== "Clave hash")}
              />
            </section>
          )}
        </>
      ) : (
        <section
          className="module-grid module-overview"
          style={{ marginTop: 28 }}
          aria-label="Secciones"
        >
          {group.entities.map((e) => (
            <article className="module-card" key={e.key}>
              <Icon name={group.icon} />
              <h2>{e.name}</h2>
              <p>Sin registros</p>
              <a href={`#${group.key}/${e.key}`} className="button">
                Abrir sección
              </a>
            </article>
          ))}
        </section>
      )}
    </>
  );
}
