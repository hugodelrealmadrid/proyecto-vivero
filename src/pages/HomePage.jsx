import Icon from "../components/Icon.jsx";
export default function HomePage() {
  return (
    <>
      <section className="public-hero">
        <div>
          <p className="eyebrow">EL JARDÍN DEL MAPACHE</p>
          <h1>
            Un pequeño brote.
            <br />
            <em>Un mundo de vida.</em>
          </h1>
          <p>
            Plantas, cuidado y jardines con personalidad. Encuentra tu próximo
            rincón verde y dale espacio para crecer.
          </p>
          <div className="hero-actions">
            <a className="button primary" href="#usuarios">
              Explorar mi jardín
            </a>
            <a className="button" href="#usuarios/plantas">
              Ver plantas
            </a>
          </div>
        </div>
        <div className="hero-emblem">
          <img
            src={`${import.meta.env.BASE_URL}logo.png`}
            alt="Mapache entre hojas, emblema de El Jardín del Mapache"
          />
        </div>
      </section>
      <section className="feature-grid" aria-label="Servicios del jardín">
        {[
          [
            "plant",
            "Plantas con personalidad",
            "Un espacio para descubrir plantas y empezar tu propio jardín.",
          ],
          [
            "tools",
            "Jardines a tu medida",
            "Proyectos, decoración y servicios para transformar tus espacios.",
          ],
          [
            "truck",
            "Más cerca de ti",
            "Un lugar para consultar tus pedidos y sus entregas.",
          ],
        ].map(([icon, name, text]) => (
          <article className="module-card" key={name}>
            <Icon name={icon} />
            <h2>{name}</h2>
            <p>{text}</p>
          </article>
        ))}
      </section>
      <section className="welcome-strip">
        <div>
          <p className="eyebrow">CADA JARDÍN TIENE SU HISTORIA</p>
          <h2>La tuya empieza aquí.</h2>
          <p>Explora tu espacio y acompaña cada etapa de tu jardín.</p>
        </div>
        <a className="button primary" href="#usuarios">
          Ir a mi espacio
        </a>
      </section>
    </>
  );
}
