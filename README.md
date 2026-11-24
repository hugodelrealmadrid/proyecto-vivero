# El Jardín del Mapache

Aplicación en **React + Vite**, diseño **Noche Jardín**, logo oficial del mapache entre hojas y autenticación local con Node.js. Pensada como base de diseño para que el equipo implemente los módulos de negocio.

## Ejecutar desde CMD

Requisitos: Node.js 22.12 o superior (recomendado 24) y npm.

```cmd
cd /d "C:\Users\alex2\OneDrive\Documentos\ChatGPT\ing software\jardin-mapache"
npm install
npm run dev
```

Abre la dirección que muestre la terminal, normalmente **http://127.0.0.1:5184/**. Mantén esa terminal abierta. Para detener el servidor, pulsa Ctrl+C. Si el puerto está ocupado, Vite indicará otro disponible.

No abras index.html con doble clic: la aplicación necesita el servidor para React y la autenticación.

Para revisar la versión compilada:

```cmd
npm run build
npm start
```

`npm start` sirve `dist/` y la API desde el mismo servidor. También se puede usar `npm run preview`. Para instalar exactamente lo registrado en package-lock.json, usa `npm ci`.

## Cuentas predeterminadas para revisar el diseño

| Rol | Correo | Contraseña |
| --- | --- | --- |
| Administrador | admin@gmail.com | admin12345 |
| Usuario | usuario@gmail.com | usuario12345 |

Se crean automáticamente la primera vez que arranca el servidor. Son cuentas de prueba solicitadas para revisar el diseño interno. Los módulos operativos permanecen vacíos.

## Páginas y separación de accesos

- `/#inicio`: página principal pública, antes de iniciar sesión.
- `/#iniciar-sesion`: formulario común de acceso. El servidor determina el rol.
- `/#registro`: registro de nuevas cuentas de usuario. Crea la cuenta e inicia su sesión.
- `/#usuarios`: portal exclusivo de usuarios registrados con sesión iniciada; plantas, pedidos, entregas, proyectos y perfil.
- `/#admin`: panel exclusivo del administrador; indicadores, gráficos y seis módulos de gestión.
- `/#personal/usuario`: lista de cuentas, accesible únicamente al administrador.
- `/#notificaciones`: avisos vacíos para la sesión actual.
- `/#logos`: galería de propuestas, dentro de administración.

El visitante no ve enlaces internos. El usuario no ve menús de administración. Escribir una ruta privada directamente no permite entrar: las rutas verifican la sesión y el rol. La API de administración también verifica el rol en el servidor, no solo en React.

## Autenticación

El registro valida nombre, correo, contraseña de 8 a 128 caracteres y confirmación. El servidor rechaza correos duplicados y asigna siempre el rol `user`. El formulario no permite registrarse como administrador.

Las contraseñas se guardan con **scrypt y sal aleatoria**, nunca en texto plano ni en localStorage. Las cuentas se conservan localmente en `.data/accounts.json`, que está excluido de Git y del ZIP de entrega. Las sesiones usan cookies HttpOnly y SameSite=Strict, duran 8 horas y se mantienen en memoria del servidor; al reiniciarlo hay que volver a iniciar sesión. Cerrar sesión invalida la sesión en el servidor.

Esta autenticación local sirve para revisar y desarrollar el proyecto. El equipo puede sustituir el archivo de cuentas por su base de datos conservando el contrato de la API. Los correos son identificadores de acceso: no se envían mensajes de correo ni se verifica su propiedad.

Las cuentas iniciales pueden configurarse mediante variables de entorno `ADMIN_EMAIL`, `ADMIN_PASSWORD`, `USER_EMAIL` y `USER_PASSWORD` antes del primer inicio. Las cuentas ya existentes no se sobrescriben al reiniciar. `COOKIE_SECURE=true` activa la marca Secure cuando se utiliza HTTPS.

## Panel del administrador

- Ventas del período, pedidos pendientes, plantas en inventario y cuentas registradas.
- Gráfico de ventas/compras con selector de serie.
- Gráfico circular de estados de tareas.
- Gráfico de barras de inventario por especie.
- Selector de últimos 7, 30 o 90 días.
- Actividad reciente y accesos a todos los módulos.

Los datos operativos comienzan en cero y los gráficos muestran **Sin datos todavía**, sin movimientos inventados. El contador de cuentas sí consulta las cuentas reales. Los componentes de gráficos aceptan datos de forma `{ label, value }`; cuando el equipo implemente sus módulos, debe agregar las consultas y agregaciones por período en `/api/admin/overview`. Por ahora el período cambia la vista vacía, porque todavía no hay registros operativos que filtrar.

## Reparto de módulos

| Módulo | Entidades |
| --- | --- |
| Plantas e inventario | Planta, lote de planta, actividad de cuidado |
| Clientes y ventas | Cliente, pedido, detalle de pedido, venta, detalle de venta, despacho de pedido |
| Entregas | Entrega, solicitud de conductor, vehículo, dirección de entrega, incidencia, reclamo |
| Compras y sucursales | Sucursal, proveedor, compra, detalle de compra, material, stock, movimiento, alerta |
| Tareas y decoración | Tarea, historial, proyecto, paquete, elemento, servicio, asignación, reserva |
| Personal | Usuario, empleado, jardinero, diseñador, conductor |

Hay páginas para 36 entidades del diagrama, más notificaciones. Las tablas tienen sus columnas y estados vacíos. La sección de cuentas ya muestra los dos accesos iniciales y las cuentas que se registren. El resto de formularios, operaciones CRUD, permisos específicos y conexiones con el esquema de negocio quedan a cargo de los responsables de cada módulo.

## Integrar el trabajo del equipo

1. Crear su carpeta en `src/modules/`, por ejemplo `src/modules/plantas/`.
2. Crear el componente React de la sección.
3. Importarlo en `src/modules/registry.jsx` y registrarlo con la clave de entidad de `src/modules.js`.

```jsx
// src/modules/plantas/Plantas.jsx
import EmptyTable from '../../components/EmptyTable.jsx';

export default function Plantas({ entity }) {
  return (
    <section className="section-panel">
      <div className="panel-heading"><h2>Plantas</h2></div>
      <EmptyTable fields={entity.fields} />
    </section>
  );
}
```

```jsx
// src/modules/registry.jsx: conservar el módulo de cuentas existente.
import Users from './personal/Users.jsx';
import Plantas from './plantas/Plantas.jsx';
export const moduleComponents = { usuario: Users, planta: Plantas };
```

La aplicación sustituye la tabla genérica por el componente registrado y conserva encabezado, menú, ruta y pestañas. Cada componente recibe `{ group, entity }`. Las claves son entidades como `planta`, `pedido`, `entrega`, `material`, `tarea` y `usuario`.

Reutilizar `EmptyTable`, `Modal`, `Icon`, `DataChart` y las clases `.section-panel`, `.panel-heading`, `.button`, `.button.primary`, `.table-scroll` para conservar el diseño. Cada compañero debe implementar validaciones, estados de carga/error, API y permisos del lado del servidor; registrar un componente no crea automáticamente su API.

Las columnas se transcribieron visualmente del diagrama. Son etiquetas de interfaz, no una migración SQL. Confirmar nombres exactos, tipos, campos opcionales y relaciones con el SQL original antes de conectar el esquema. La clave hash de usuario nunca se muestra en las tablas.

## Archivos principales

- `src/pages/HomePage.jsx`: página principal pública.
- `src/pages/AuthPage.jsx`: acceso y registro.
- `src/pages/UserPage.jsx`: portal del usuario.
- `src/pages/Dashboard.jsx`: panel del administrador.
- `src/components/AdminOverview.jsx`: indicadores y filtros de gráficos.
- `src/components/DataChart.jsx`: gráficos reutilizables con SVG y CSS.
- `src/auth/`: estado de sesión, cliente API y protección de rutas.
- `src/components/Layout.jsx`: encabezado y navegación según rol.
- `src/modules.js`: catálogo de módulos y columnas.
- `src/modules/registry.jsx`: integración del equipo.
- `src/styles.css`: tema y diseño adaptable.
- `server/auth.js`: cuentas, sesiones, registro, acceso y API protegida.
- `server/index.js`: servidor para la versión compilada.
- `public/logo.png`: logo oficial transparente.
- `public/logos/`: cinco propuestas PNG con transparencia.
- `tests/auth.test.js`: pruebas de autenticación y permisos.

## API disponible

| Método y ruta | Acceso |
| --- | --- |
| GET /api/auth/me | Sesión actual o null |
| POST /api/auth/login | Correo y contraseña |
| POST /api/auth/register | Nombre, correo y contraseña; crea rol user |
| POST /api/auth/logout | Cierre de sesión |
| GET /api/admin/users | Solo admin, cuentas sin hash ni sal |
| GET /api/admin/overview | Solo admin, resumen y series vacías |
| GET /api/user/profile | Solo user, perfil de la sesión |

## Paleta Noche Jardín

Fondo `#10231D`, superficie `#1C382E`, acento `#A3D65C`, texto `#EDF5E9` y salvia `#7F9B8D`.

## Logos

Se generaron cinco propuestas con la herramienta integrada de generación de imágenes. La primera es el logo elegido y está en `public/logo.png` y `public/logos/logo-1.png`. Los prompts completos están en `LOGOS-PROMPTS.md`. Todos son PNG con canal alfa; el tablero cuadriculado de algunas vistas previas no forma parte del original.

## Verificar

```cmd
npm test
npm run build
```

Las pruebas usan una carpeta temporal independiente: no agregan cuentas de prueba adicionales al proyecto.

Entrega local con código fuente y recursos. No requiere servicios externos para ejecutarse.
