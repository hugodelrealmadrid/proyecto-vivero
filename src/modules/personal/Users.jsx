import { useEffect, useState } from "react";
import { request } from "../../auth/api.js";
export default function Users() {
  const [users, setUsers] = useState([]),
    [loading, setLoading] = useState(true),
    [error, setError] = useState("");
  useEffect(() => {
    let active = true;
    request("/api/admin/users")
      .then((data) => {
        if (active) setUsers(data.users);
      })
      .catch((err) => {
        if (active) setError(err.message);
      })
      .finally(() => {
        if (active) setLoading(false);
      });
    return () => {
      active = false;
    };
  }, []);
  return (
    <section className="section-panel">
      <div className="panel-heading">
        <h2>Cuentas registradas</h2>
        <span className="record-count">{users.length} cuentas</span>
      </div>
      {loading ? (
        <p role="status">Cargando cuentas…</p>
      ) : error ? (
        <p role="alert">{error}</p>
      ) : (
        <div className="table-scroll">
          <table>
            <thead>
              <tr>
                {["Nombre", "Correo", "Rol", "Fecha de registro"].map((h) => (
                  <th key={h} scope="col">
                    {h}
                  </th>
                ))}
              </tr>
            </thead>
            <tbody>
              {users.map((user) => (
                <tr key={user.id}>
                  <td>{user.name}</td>
                  <td>{user.email}</td>
                  <td>{user.role === "admin" ? "Administrador" : "Usuario"}</td>
                  <td>
                    {new Date(user.createdAt).toLocaleDateString("es-BO")}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </section>
  );
}
