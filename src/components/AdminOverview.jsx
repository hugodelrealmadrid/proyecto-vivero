import { useEffect, useState } from "react";
import { request } from "../auth/api.js";
import Icon from "./Icon.jsx";
import DataChart from "./DataChart.jsx";

export default function AdminOverview() {
  const [period, setPeriod] = useState("30"),
    [series, setSeries] = useState("sales");
  const [data, setData] = useState(null),
    [error, setError] = useState("");
  useEffect(() => {
    let active = true;
    setError("");
    request(`/api/admin/overview?days=${period}`)
      .then((value) => {
        if (active) setData(value);
      })
      .catch((err) => {
        if (active) setError(err.message);
      });
    return () => {
      active = false;
    };
  }, [period]);
  const cards = [
    ["cart", "Ventas del período", data?.summary.sales, "Bs"],
    ["truck", "Pedidos pendientes", data?.summary.pendingOrders, ""],
    ["plant", "Plantas en inventario", data?.summary.plants, ""],
    ["people", "Cuentas registradas", data?.accounts, ""],
  ];
  return (
    <section aria-label="Resumen y gráficos de administración">
      <div className="overview-toolbar">
        <div>
          <h2>El pulso de tu jardín</h2>
          <p>Una vista general de la actividad del vivero.</p>
        </div>
        <label className="period-control">
          Período
          <select value={period} onChange={(e) => setPeriod(e.target.value)}>
            <option value="7">Últimos 7 días</option>
            <option value="30">Últimos 30 días</option>
            <option value="90">Últimos 90 días</option>
          </select>
        </label>
      </div>
      {error && (
        <p role="alert" className="form-message error">
          {error}
        </p>
      )}
      <div className="stats-grid">
        {cards.map(([icon, title, value, unit]) => (
          <article className="stat-card" key={title}>
            <div>
              <p>{title}</p>
              <strong>
                {value === undefined
                  ? "—"
                  : `${unit ? unit + " " : ""}${Number(value).toLocaleString("es-BO")}`}
              </strong>
              <span>
                {title === "Cuentas registradas"
                  ? "Accesos al sistema"
                  : "Sin registros operativos"}
              </span>
            </div>
            <Icon name={icon} />
          </article>
        ))}
      </div>
      <div className="charts-grid">
        <article className="section-panel sales-chart">
          <div className="panel-heading">
            <div>
              <h2>Ventas y compras</h2>
              <p>Movimientos del período seleccionado.</p>
            </div>
            <div className="chart-switch" aria-label="Serie del gráfico">
              <button
                className={series === "sales" ? "active" : ""}
                onClick={() => setSeries("sales")}
                aria-pressed={series === "sales"}
              >
                Ventas
              </button>
              <button
                className={series === "purchases" ? "active" : ""}
                onClick={() => setSeries("purchases")}
                aria-pressed={series === "purchases"}
              >
                Compras
              </button>
            </div>
          </div>
          <DataChart
            data={data?.[series] || []}
            label={series === "sales" ? "Ventas" : "Compras"}
            period={period}
          />
          <div className="chart-caption">
            {series === "sales" ? "Ingresos por ventas" : "Gastos en compras"} ·
            Últimos {period} días
          </div>
        </article>
        <article className="section-panel">
          <div className="panel-heading">
            <div>
              <h2>Estado de tareas</h2>
              <p>Distribución del trabajo del equipo.</p>
            </div>
            <Icon name="tools" />
          </div>
          <DataChart
            type="donut"
            data={data?.tasks || []}
            label="Estado de tareas"
          />
        </article>
        <article className="section-panel inventory-chart">
          <div className="panel-heading">
            <div>
              <h2>Inventario por especie</h2>
              <p>Disponibilidad actual de plantas.</p>
            </div>
            <a className="button" href="#plantas">
              Ver inventario
            </a>
          </div>
          <DataChart
            type="bar"
            data={data?.stock || []}
            label="Inventario por especie"
            period={period}
          />
        </article>
      </div>
    </section>
  );
}
