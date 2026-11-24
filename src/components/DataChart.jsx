import { useId } from "react";

/** Reutilizable: data = [{label:'Enero', value:150}]. Sin valores inventados. */
export default function DataChart({ type = "line", data = [], label, period }) {
  const titleId = useId();
  const values = data.map((item) => ({
    ...item,
    value: Number(item.value) || 0,
  }));
  const hasData = values.some((item) => item.value > 0);
  if (type === "donut") {
    const total = values.reduce(
      (sum, item) => sum + Math.max(0, item.value),
      0,
    );
    const colors = ["#a3d65c", "#7f9b8d", "#e1b871", "#6bb6a6"];
    let angle = 0;
    const segments = values.map((item, index) => {
      const start = angle;
      angle += (Math.max(0, item.value) / total) * 360;
      return `${colors[index % colors.length]} ${start}deg ${angle}deg`;
    });
    return (
      <div
        className="donut-chart"
        role="img"
        aria-label={`${label}: ${hasData ? `${total} tareas` : "sin datos"}`}
      >
        <div
          className="donut-ring"
          style={
            hasData
              ? { background: `conic-gradient(${segments.join(",")})` }
              : undefined
          }
        >
          <div>
            <strong>{hasData ? total : "—"}</strong>
            <span>{hasData ? "Tareas" : "Sin datos"}</span>
          </div>
        </div>
        <div className="chart-legend">
          {hasData ? (
            values.map((item, i) => (
              <span key={item.label}>
                <i style={{ background: colors[i % colors.length] }} />
                {item.label}: {item.value}
              </span>
            ))
          ) : (
            <span>Los estados aparecerán al registrar tareas.</span>
          )}
        </div>
      </div>
    );
  }
  const width = 600,
    height = 220,
    pad = 35,
    max = Math.max(1, ...values.map((item) => item.value));
  const x = (i) =>
    pad + ((i + 0.5) / Math.max(1, values.length)) * (width - 2 * pad);
  const y = (value) => height - pad - (value / max) * (height - pad * 2);
  return (
    <div className="chart-canvas">
      <svg
        className="data-chart"
        viewBox={`0 0 ${width} ${height}`}
        role="img"
        aria-labelledby={titleId}
      >
        <title id={titleId}>
          {label}:{" "}
          {hasData
            ? values.map((item) => `${item.label}: ${item.value}`).join(", ")
            : `sin datos en los últimos ${period} días`}
        </title>
        {[0, 1, 2, 3].map((i) => (
          <line
            className="chart-gridline"
            key={i}
            x1={pad}
            x2={width - pad}
            y1={pad + (i * (height - pad * 2)) / 3}
            y2={pad + (i * (height - pad * 2)) / 3}
          />
        ))}
        {hasData &&
          (type === "bar" ? (
            values.map((item, i) => (
              <rect
                key={item.label}
                className="chart-bar"
                x={x(i) - Math.min(20, (width - pad * 2) / values.length / 3)}
                y={y(item.value)}
                width={Math.min(40, ((width - pad * 2) / values.length) * 0.6)}
                height={height - pad - y(item.value)}
                rx="5"
              />
            ))
          ) : (
            <>
              <polyline
                className="chart-line"
                points={values
                  .map((item, i) => `${x(i)},${y(item.value)}`)
                  .join(" ")}
              />
              {values.map((item, i) => (
                <circle
                  key={item.label}
                  className="chart-point"
                  cx={x(i)}
                  cy={y(item.value)}
                  r="4"
                />
              ))}
            </>
          ))}
        {hasData &&
          values.map((item, i) => (
            <text
              key={item.label}
              x={x(i)}
              y={height - 8}
              textAnchor="middle"
              className="chart-axis-label"
            >
              {item.label}
            </text>
          ))}
      </svg>
      {!hasData && (
        <div className="chart-empty">
          <strong>Sin datos todavía</strong>
          <span>
            {type === "bar"
              ? "El inventario se mostrará por especie."
              : `No hay movimientos en los últimos ${period} días.`}
          </span>
        </div>
      )}
    </div>
  );
}
