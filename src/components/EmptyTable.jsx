import Icon from "./Icon.jsx";
export default function EmptyTable({
  fields,
  title = "Sin registros",
  description = "Aún no hay información para mostrar.",
}) {
  return (
    <div className="table-scroll">
      <table>
        <thead>
          <tr>
            {fields.map((field) => (
              <th key={field} scope="col">
                {field}
              </th>
            ))}
          </tr>
        </thead>
        <tbody>
          <tr>
            <td className="empty-cell" colSpan={fields.length}>
              <span className="empty-icon">
                <Icon />
              </span>
              <strong>{title}</strong>
              <p>{description}</p>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  );
}
