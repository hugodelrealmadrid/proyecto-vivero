import { modules } from "../modules.js";
import Icon from "../components/Icon.jsx";
import EmptyTable from "../components/EmptyTable.jsx";
import AdminOverview from "../components/AdminOverview.jsx";
export default function Dashboard() {
  return (
    <>
      <section className="hero">
        <div>
          <p className="eyebrow">ADMINISTRACIÓN · UN ESPACIO PARA CRECER</p>
          <h1>Todo tu jardín, conectado.</h1>
          <p>
            Administra plantas, ventas, entregas, compras, tareas y tu equipo.
          </p>
        </div>
        <div className="hero-botanical" aria-hidden="true">
          <Icon />
          <Icon />
          <Icon />
        </div>
      </section>
      <AdminOverview />
      <div className="module-section-heading">
        <h2>Módulos del vivero</h2>
        <p>Todo lo necesario para organizar tu jardín.</p>
      </div>
      <section className="module-grid" aria-label="Módulos del sistema">
        {modules.map((g) => (
          <article className="module-card" key={g.key}>
            <div className="card-title">
              <Icon name={g.icon} />
              <h2>{g.name}</h2>
            </div>
            <ul className="entity-links">
              {g.entities.map((e) => (
                <li key={e.key}>
                  <a href={`#${g.key}/${e.key}`}>
                    <span>{e.name}</span>
                    <span className="dash" aria-hidden="true">
                      —
                    </span>
                  </a>
                </li>
              ))}
            </ul>
            <a className="button" href={`#${g.key}`}>
              Ir al módulo
            </a>
          </article>
        ))}
      </section>
      <section className="section-panel">
        <div className="panel-heading">
          <div>
            <h2 className="with-icon">
              <Icon name="activity" />
              Actividad reciente
            </h2>
            <p>Los movimientos de tu jardín, en un solo lugar.</p>
          </div>
          <span className="record-count">0 registros</span>
        </div>
        <EmptyTable
          fields={[
            "Fecha y hora",
            "Módulo",
            "Actividad",
            "Detalle",
            "Responsable",
          ]}
          description="Aún no hay actividad registrada en el sistema."
        />
      </section>
    </>
  );
}
