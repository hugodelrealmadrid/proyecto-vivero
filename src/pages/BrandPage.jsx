const logos = [
  ["Rostro entre hojas", "El logo oficial que elegiste."],
  ["Mapache jardinero", "Una mascota con una planta en sus manos."],
  ["Jardín de luna", "Un emblema botánico nocturno."],
  ["Brote creativo", "Una composición orgánica."],
  ["Escudo del jardín", "Una insignia con personalidad."],
];
export default function BrandPage() {
  return (
    <>
      <a className="back-link" href="#inicio">
        Volver al inicio
      </a>
      <div className="page-heading">
        <div>
          <p className="eyebrow">IDENTIDAD VISUAL</p>
          <h1>Un mapache con raíces.</h1>
          <p>
            El rostro entre hojas es nuestro logo principal. Las otras
            propuestas quedan disponibles para el equipo.
          </p>
        </div>
      </div>
      <section className="logo-gallery" style={{ marginTop: 30 }}>
        {logos.map(([name, description], i) => (
          <article className="logo-option" key={name}>
            <img
              src={`${import.meta.env.BASE_URL}logos/logo-${i + 1}.png`}
              alt={name}
              loading="lazy"
            />
            <h2>{name}</h2>
            <p className="notice">{description}</p>
            <a
              className="button"
              href={`${import.meta.env.BASE_URL}logos/logo-${i + 1}.png`}
              download
            >
              Descargar PNG
            </a>
          </article>
        ))}
      </section>
    </>
  );
}
