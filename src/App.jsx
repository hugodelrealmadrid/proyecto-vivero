import { useEffect, useState } from "react";
import Layout from "./components/Layout.jsx";
import Modal from "./components/Modal.jsx";
import EmptyTable from "./components/EmptyTable.jsx";
import Dashboard from "./pages/Dashboard.jsx";
import ModulePage from "./pages/ModulePage.jsx";
import BrandPage from "./pages/BrandPage.jsx";
import HomePage from "./pages/HomePage.jsx";
import UserPage from "./pages/UserPage.jsx";
import AuthPage from "./pages/AuthPage.jsx";
import { modules, notifications } from "./modules.js";
import RequireRole from "./auth/RequireRole.jsx";
import { useAuth } from "./auth/AuthContext.jsx";
export default function App() {
  const { user } = useAuth();
  const [route, setRoute] = useState(
    () => window.location.hash.slice(1) || "inicio",
  );
  const [dialog, setDialog] = useState(null);
  useEffect(() => {
    const change = () => {
      setRoute(window.location.hash.slice(1) || "inicio");
      setDialog(null);
    };
    window.addEventListener("hashchange", change);
    return () => window.removeEventListener("hashchange", change);
  }, []);
  const [groupKey, entityKey] = route.split("/");
  const group = modules.find((g) => g.key === groupKey),
    entity = group?.entities.find((e) => e.key === entityKey);
  const validGroup =
    group && (!entityKey || entity) && route.split("/").length <= 2;
  useEffect(() => {
    document.title = `${route === "inicio" ? "Principal" : route === "admin" ? "Administración" : route === "iniciar-sesion" ? "Iniciar sesión" : route === "registro" ? "Crear cuenta" : groupKey === "usuarios" ? "Usuarios" : entity?.name || group?.name || (route === "notificaciones" ? "Notificaciones" : route === "logos" ? "Identidad visual" : "Página no encontrada")} · El Jardín del Mapache`;
  }, [route, entity, group, groupKey]);
  let content;
  if (route === "inicio") content = <HomePage />;
  else if (route === "iniciar-sesion") content = <AuthPage key="login" />;
  else if (route === "registro") content = <AuthPage key="register" register />;
  else if (route === "admin") content = <Dashboard />;
  else if (groupKey === "usuarios" && route.split("/").length <= 2)
    content = <UserPage sectionKey={entityKey} />;
  else if (route === "logos") content = <BrandPage />;
  else if (route === "notificaciones")
    content = (
      <>
        <a href="#inicio" className="back-link">
          Volver al inicio
        </a>
        <div className="page-heading">
          <div>
            <p className="eyebrow">TU JARDÍN AL DÍA</p>
            <h1>Notificaciones</h1>
            <p>Avisos y novedades del sistema.</p>
          </div>
        </div>
        <section className="section-panel">
          <EmptyTable
            fields={notifications.fields}
            title="Sin notificaciones"
            description="Los nuevos avisos aparecerán aquí."
          />
        </section>
      </>
    );
  else if (validGroup) content = <ModulePage group={group} entity={entity} />;
  else
    content = (
      <section className="hero">
        <div>
          <h1>Esta página no está en el jardín.</h1>
          <p>Comprueba la dirección o vuelve al inicio.</p>
          <a className="button" style={{ marginTop: 20 }} href="#inicio">
            Volver al inicio
          </a>
        </div>
      </section>
    );
  if (route === "admin" || validGroup || route === "logos")
    content = <RequireRole role="admin">{content}</RequireRole>;
  else if (groupKey === "usuarios")
    content = <RequireRole role="user">{content}</RequireRole>;
  else if (route === "notificaciones")
    content = <RequireRole role={user?.role || "user"}>{content}</RequireRole>;
  return (
    <Layout route={route} onDialog={setDialog}>
      {content}
      {dialog && (
        <Modal
          title={dialog === "help" ? "¿Cómo recorrer tu jardín?" : "Mi cuenta"}
          onClose={() => setDialog(null)}
        >
          {dialog === "help" ? (
            <>
              <p>
                Elige un módulo desde las tarjetas de inicio o la barra
                superior. Cada módulo reúne sus secciones relacionadas.
              </p>
              <p>
                Usa el buscador para encontrar una sección por su nombre. Pulsa{" "}
                <kbd>/</kbd> para buscar y <kbd>Esc</kbd> para cerrar.
              </p>
              <p>
                Las secciones comienzan vacías. Sus registros se mostrarán
                cuando cada módulo esté conectado.
              </p>
            </>
          ) : (
            <>
              <p>No hay una sesión iniciada.</p>
              <p>
                El acceso de usuarios y los permisos estarán disponibles cuando
                se conecte el módulo de autenticación.
              </p>
            </>
          )}
        </Modal>
      )}
    </Layout>
  );
}
