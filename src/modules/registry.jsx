/** Integración de módulos:
 * import Plantas from './plantas/Plantas.jsx';
 * export const moduleComponents = { planta: Plantas };
 * Cada componente recibe {group, entity}. Las claves están en src/modules.js.
 * Sin un componente registrado se muestra la tabla vacía del diseño.
 * El equipo implementará aquí formularios, operaciones, API y permisos.
 */
import Users from "./personal/Users.jsx";
export const moduleComponents = { usuario: Users };
