# Guía de trabajo para los 4 equipos (Code³, FRIENDS, Dinamita, Mapache) – Sistema de Gestión para Vivero

Fecha final de presentación: **viernes 4 de diciembre de 2026**

Lee esta guía completa antes de escribir código. Si todos seguimos estas reglas,
el merge final sale **sin conflictos**.

---

## 1. Qué hay en el proyecto

| Tecnología | Versión |
|---|---|
| ASP.NET Core MVC | .NET 8 (Visual Studio 2022) |
| Base de datos | MySQL 8 |
| Acceso a datos | Entity Framework Core 8 + Pomelo.EntityFrameworkCore.MySql |
| Login | Cookies + roles (tabla `Usuario` y `Rol`) |

```
SistemaVivero/
├── database/vivero_db.sql        ← la BD COMPLETA (todas las tablas de todos los equipos)
├── GUIA_EQUIPOS.md               ← esta guía
└── Vivero.Web/
    ├── Program.cs                ← COMPARTIDO (no tocar)
    ├── Data/ViveroDbContext.cs   ← COMPARTIDO (no tocar) – ya tiene TODAS las tablas
    ├── Helpers/RolesSistema.cs   ← nombres de roles y datos del usuario logueado
    ├── Services/NotificacionService.cs ← notificaciones y bitácora (para todos)
    ├── Models/                   ← COMPARTIDO – una clase por tabla
    │   ├── Comun/  Inventario/  Personal/  Plantas/  Tareas/  Paquetes/  Ventas/  Entregas/
    ├── Views/Shared/_Layout.cshtml, _MenuModulos.cshtml ← COMPARTIDO
    └── Areas/                    ← AQUÍ TRABAJA CADA EQUIPO
        ├── Sucursales/ (Mapache)     ├── Personal/  (Code³)      ├── Tareas/    (FRIENDS)    ├── Ventas/    (Dinamita)
        ├── Inventario/ (Mapache)     ├── Plantas/   (Code³)      ├── Paquetes/  (FRIENDS)    ├── Entregas/  (Dinamita)
```

Cada **caso de uso ya tiene su propio controlador y su carpeta de vistas** creados
(con una pantalla "en construcción"). Ejemplo para UC4.1:

```
Areas/Tareas/Controllers/RegistrarTareaController.cs   ← solo lo toca Mayerli
Areas/Tareas/Views/RegistrarTarea/Index.cshtml         ← solo lo toca Mayerli
Areas/Tareas/ViewModels/RegistrarTareaViewModel.cs     ← (lo crea Mayerli si lo necesita)
```

**Como nadie edita el archivo de otro, Git no tiene nada que "mezclar" y no hay conflictos.**

---

## 2. Instalar y ejecutar (cada integrante, una sola vez)

1. Instalar **Visual Studio 2022** (carga de trabajo "ASP.NET y desarrollo web") y **XAMPP** (o MySQL 8 con Workbench: funcionan los dos).
2. Clonar el repositorio:
   ```bash
   git clone <URL-DEL-REPO>
   cd SistemaVivero
   ```
3. Crear la base de datos:
   - **Con XAMPP:** abre el panel de XAMPP → *Start* en **MySQL** (y en Apache para phpMyAdmin) → entra a
     `http://localhost/phpmyadmin` → pestaña **Importar** (sin seleccionar ninguna BD) → elige
     `database/vivero_db.sql` → **Importar**. Se crea `vivero_db` con todas las tablas.
   - **Con MySQL Workbench:** *File > Open SQL Script* → `database/vivero_db.sql` → ejecutar (⚡).
4. Contraseña de MySQL:
   - **XAMPP** viene con usuario `root` **sin contraseña**: no tienes que hacer nada, `appsettings.json` ya está así.
   - Si tu MySQL **sí** tiene contraseña (Workbench) o usas otro puerto: copia `Vivero.Web/appsettings.Local.ejemplo.json`,
     renómbralo `appsettings.Local.json` y escribe tus datos. Ese archivo **no se sube a Git**.
     **Nunca** pongas tu contraseña en `appsettings.json`.
   - **XAMPP/MySQL tiene que estar encendido** antes de dar F5, si no el sistema no arranca.
5. Abrir `SistemaVivero.sln` en Visual Studio → ejecutar (F5). La primera vez descarga los paquetes NuGet.
6. Iniciar sesión con un usuario de prueba (contraseña de todos: **`Vivero123*`**):

| Usuario | Rol | Para probar |
|---|---|---|
| `admin` | Administrador | todo |
| `supervisor` | Supervisor | personal, plantas, tareas, ventas, entregas |
| `jardinero` | Jardinero | plantas (CU04, CU06), consultar tareas |
| `disenador` | Diseñador | paquetes |
| `conductor` | Conductor | entregas |
| `cliente` | Cliente | catálogo, pedidos, confirmar recepción |

---

## 3. Qué hace cada equipo (y qué archivo es de quién)

### Equipo Code³ – Gestión de Personal y Gestión de Plantas
| CU | Responsable | Controlador (Areas/…) | Tablas |
|---|---|---|---|
| CU01 Registrar personal y asignar puesto | Brener Garcia | `Personal/RegistrarPersonalController` | Trabajador, Puesto, Sucursal, Especialidad |
| CU02 Actualizar datos del personal | Emily Condori | `Personal/ActualizarPersonalController` | Trabajador |
| CU03 Dar de baja al personal | Sidny Mendoza | `Personal/BajaPersonalController` | Trabajador (Estado = 'Inactivo'), Bitacora |
| CU04 Registrar planta | Brener Garcia | `Plantas/RegistrarPlantaController` | Planta |
| CU05 Actualizar cantidad y estado | Emily Condori | `Plantas/ActualizarPlantaController` | Planta, HistorialPlanta |
| CU06 Registrar actividades / reportes | Ariel Rocabado | `Plantas/ActividadPlantaController` | ActividadPlanta, TipoActividad |
| CU07 Dar de baja planta o lote | Sidny Mendoza | `Plantas/BajaPlantaController` | Planta (EstadoRegistro = 'De baja') |
| CU08 Orden de compra de suministros | Ariel Rocabado | `Plantas/OrdenCompraController` | OrdenCompra, DetalleOrdenCompra, Material |

### Equipo FRIENDS – Asignación de Tareas y Paquetes de Decoración
| CU | Responsable | Controlador (Areas/…) | Tablas |
|---|---|---|---|
| UC4.1 Registrar tarea | Mayerli Quinteros | `Tareas/RegistrarTareaController` | Tarea, TareaMaterial, ReservaMaterial, HistorialTarea, Notificacion |
| UC4.2 Consultar estado de tarea | Maya Mercado | `Tareas/ConsultarTareaController` | Tarea, HistorialTarea |
| UC4.3 Modificar / reasignar tarea | José Yanarico | `Tareas/ModificarTareaController` | Tarea, HistorialTarea, ReservaMaterial |
| UC4.4 Eliminar / cancelar tarea | Hugo García | `Tareas/CancelarTareaController` | Tarea, ReservaMaterial, Bitacora |
| UC4.5 Visualizar trabajador | Mayerli Quinteros | `Tareas/VisualizarTrabajadorController` | Trabajador, Tarea |
| UC6.1 Crear paquete (precio) | Maya Mercado | `Paquetes/CrearPaqueteController` | Paquete, PaqueteDetalle, PaqueteImagen |
| UC6.2 Crear paquete personalizado | José Yanarico | `Paquetes/PaquetePersonalizadoController` | Paquete, ReservaMaterial, Cotizacion |
| UC6.3 Consultar catálogo de paquetes | Hugo García | `Paquetes/CatalogoPaquetesController` | Paquete (Estado = 'Disponible') |
| UC6.4 Modificar paquete | Mayerli Quinteros | `Paquetes/ModificarPaqueteController` | Paquete, ReservaMaterial, Cotizacion, Bitacora |
| UC6.5 Eliminar paquete | Maya Mercado | `Paquetes/EliminarPaqueteController` | Paquete, Bitacora |
| UC6.6 Asignar paquete a proyecto/cliente | José Yanarico | `Paquetes/AsignarPaqueteController` | AsignacionPaquete, Proyecto, Cotizacion |

### Equipo Dinamita – Ventas/Pedidos y Gestión de Entregas
| CU | Responsable | Controlador (Areas/…) | Tablas |
|---|---|---|---|
| Registrar venta en vivero | Stif Lazo | `Ventas/RegistrarVentaController` | Venta, DetalleVenta, Planta (stock) |
| Ver historial de ventas | Stif Lazo | `Ventas/HistorialVentasController` | Venta |
| Ver reporte de ventas | Stif Lazo | `Ventas/ReporteVentasController` | Venta, DetalleVenta |
| Registrar cliente | Stif Lazo | `Ventas/RegistrarClienteController` | Cliente |
| Actualizar datos de cliente | Stif Lazo | `Ventas/ActualizarClienteController` | Cliente |
| Realizar pedido en línea | Jhamil Flores | `Ventas/RealizarPedidoController` | Pedido, DetallePedido |
| Cancelar pedido | Jhamil Flores | `Ventas/CancelarPedidoController` | Pedido |
| Consultar catálogo de plantas | Jhamil Flores | `Ventas/CatalogoPlantasController` | Planta |
| Historial de compras del cliente | Jhamil Flores | `Ventas/HistorialComprasController` | Pedido, DetallePedido |
| Revisar y aprobar pedido en línea | Jhamil Flores | `Ventas/AprobarPedidoController` | Pedido |
| Registrar entrega | Franz Villarroel | `Entregas/RegistrarEntregaController` | Entrega, DireccionEntrega |
| Asignar vehículo | Franz Villarroel | `Entregas/AsignarVehiculoController` | Entrega, Vehiculo |
| Asignar conductor | Franz Villarroel | `Entregas/AsignarConductorController` | Entrega, Conductor, SolicitudConductor |
| Ver entregas pendientes | Franz Villarroel | `Entregas/EntregasPendientesController` | Entrega |
| Actualizar estado de entrega | Jhery Rodriguez | `Entregas/ActualizarEstadoEntregaController` | Entrega |
| Registrar incidencia | Jhery Rodriguez | `Entregas/RegistrarIncidenciaController` | Incidencia |
| Confirmar recepción | Jhery Rodriguez | `Entregas/ConfirmarRecepcionController` | Entrega, Reclamo |
| Ver reporte de entregas | Jhery Rodriguez | `Entregas/ReporteEntregasController` | Entrega (ReporteEntrega = ViewModel, no tabla) |

> En el diagrama de clases de Dinamita, **Producto = `Planta`** y **Supervisor / EncargadoSucursal = `Trabajador`**
> (con puesto Supervisor). Así se evita tener la misma información en dos tablas.

### Grupo Mapache – Gestión de Sucursales y Gestión de Material y Proveedores
| CU | Responsable | Controlador (Areas/…) | Tablas |
|---|---|---|---|
| UC1 Registrar sucursal | Alaitz Felipez | `Sucursales/RegistrarSucursalController` | Sucursal (Codigo SUC-001…), Bitacora |
| UC2 Editar datos de sucursal | Alaitz Felipez | `Sucursales/EditarSucursalController` | Sucursal, Bitacora |
| UC3 Dar de baja sucursal | Alaitz Felipez | `Sucursales/BajaSucursalController` | Sucursal (Estado = 'Inactiva'), Trabajador, StockMaterial, MovimientoStock |
| UC4 Asignar personal a sucursal | Alaitz Felipez | `Sucursales/AsignarPersonalController` | Trabajador.IdSucursal, Usuario.IdRol |
| UC5 Listado de sucursales | Yubidsa Bueno | `Sucursales/ListadoSucursalesController` | Sucursal, Trabajador |
| UC6 Registrar proveedor | Yubidsa Bueno | `Inventario/RegistrarProveedorController` | Proveedor |
| UC7 Editar proveedor | Yubidsa Bueno | `Inventario/EditarProveedorController` | Proveedor |
| UC8 Registrar compra de material | Yubidsa Bueno | `Inventario/RegistrarCompraController` | Compra, DetalleCompra, StockMaterial, MovimientoStock |
| UC9 Consultar stock | Roberto Herrera | `Inventario/StockMaterialesController` | StockMaterial, Material |
| UC10 Dar de baja proveedor | Roberto Herrera | `Inventario/BajaProveedorController` | Proveedor (Estado = 'Inactivo'), Compra |
| UC11 Notificar bajo stock | Roberto Herrera | `Inventario/AlertasStockController` + `Inventario/Services/VerificadorStockBajoService.cs` (automático, cada 24 h) | AlertaStock, StockMaterial |
| UC12 Configurar umbral mínimo | Roberto Herrera | `Inventario/UmbralMaterialController` | StockMaterial.UmbralMinimo |

> En el diagrama de Mapache, **Empleado (Supervisor, Jardinero, DiseñadorDeAmbientes, Conductor) = `Trabajador`**
> (su tipo es el `Puesto`, la especialidad es `Especialidad` y la licencia está en `Conductor`).
> **Usuario/Administrador** = tabla `Usuario` con su `Rol`. **EstadoCompra** = columna `Compra.Estado`.
> **Stock** = `StockMaterial` (una fila por material y sucursal) y **Alerta** = `AlertaStock`.

### Cómo quedaron las partes que se cruzaban entre equipos
- **Registrar trabajador vs. asignarlo a sucursal:** Code³ (CU01) registra al trabajador con sus datos y puesto; la sucursal
  puede quedar vacía. Mapache (UC4) es quien lo asigna o reasigna a una sucursal.
- **Orden de compra (Code³ CU08) vs. Compra (Mapache UC8):** la *orden* es el pedido de lo que se necesita (no mueve stock).
  La *compra* es cuando se compró de verdad: suma al stock y genera comprobante. Una compra puede venir de una orden
  (`Compra.IdOrdenCompra`) y entonces la orden pasa a `'Comprada'`. Ambos usan el mismo catálogo de `Proveedor`.
- **Stock de materiales:** ya no hay un stock único; el stock está **por sucursal** en `StockMaterial`. FRIENDS reserva
  material de la sucursal de la tarea (`Tarea.IdSucursal`) o del paquete (`Paquete.IdSucursal`).
- **Sucursal inactiva:** ningún equipo debe permitir operaciones nuevas en una sucursal con `Estado = 'Inactiva'`
  (al llenar listas desplegables de sucursales, filtra `s.Estado == "Activa"`).

### Dependencias entre equipos (quién usa datos de quién)
- **Sucursal** la crea Mapache → la usan todos.
- **Trabajador** lo crea Code³ → Mapache lo asigna a sucursal; lo usan FRIENDS (asignar tareas) y Dinamita (supervisor, conductor).
- **Planta** la crea Code³ → la usan Dinamita (ventas/pedidos/catálogo) y FRIENDS (paquetes).
- **Cliente** lo crea Dinamita → lo usa FRIENDS (paquetes personalizados, asignación).
- **Material / StockMaterial / Proveedor** los maneja Mapache → los usan Code³ (órdenes de compra) y FRIENDS (reservas).

Mientras el otro equipo termina su parte, **usa los datos de prueba** que ya trae `vivero_db.sql`
(2 sucursales, 9 trabajadores, 6 plantas, 3 clientes, 7 materiales con stock por sucursal, 3 proveedores,
4 servicios, vehículos y un conductor). El insecticida de la Sucursal Central ya está bajo su umbral, para probar las alertas.
No esperes a nadie.

---

## 4. Reglas para que el merge NO falle

### 4.1 Archivos que NADIE modifica en su rama
`Program.cs`, `Data/ViveroDbContext.cs`, `Models/**`, `Helpers/**`, `Services/**`,
`Views/Shared/**`, `database/vivero_db.sql`, `Vivero.Web.csproj`, `appsettings.json`, `.gitignore`.

¿Necesitas una columna, tabla, rol o paquete NuGet nuevo? → **Pídelo por WhatsApp/Trello a la encargada de la BD**.
Ella lo agrega en `main`/`develop` y todos hacen `git pull`. **No uses `Add-Migration` ni `Update-Database`.**

### 4.2 Solo tocas TUS archivos
- `Areas/<Modulo>/Controllers/<TuCU>Controller.cs`
- `Areas/<Modulo>/Views/<TuCU>/*.cshtml` (puedes crear más vistas: `Crear.cshtml`, `Detalle.cshtml`, …)
- `Areas/<Modulo>/ViewModels/<TuCU>ViewModel.cs` (archivos nuevos, con nombre de tu CU)
- La página `Areas/<Modulo>/Views/Inicio/Index.cshtml` ya tiene los botones a todos los CU: **no hace falta editarla**.

Si dos CU necesitan lo mismo (por ejemplo, UC4.1 abre UC4.5), **se enlazan con un link o redirect**,
no copiando código del otro:
```csharp
return RedirectToAction("Index", "VisualizarTrabajador", new { area = "Tareas", fecha = fechaProgramada });
```

### 4.3 Ramas de Git
```
main                ← versión que se presenta (solo la encargada hace merge aquí)
 └── develop        ← integración de todos los equipos
      ├── equipo/code3
      ├── equipo/friends
      ├── equipo/dinamita
      └── equipo/mapache
           └── cu/<codigo>-<nombre>   ← rama personal de cada integrante
```
Ejemplo de nombre de rama personal: `cu/uc4.1-registrar-tarea`, `cu/cu05-actualizar-planta`.

### 4.4 Flujo diario de cada integrante
```bash
# 1. Una sola vez: crear tu rama a partir de la de tu equipo
git checkout equipo/friends
git pull
git checkout -b cu/uc4.1-registrar-tarea

# 2. Cada día ANTES de empezar: traer lo nuevo
git checkout equipo/friends && git pull
git checkout cu/uc4.1-registrar-tarea
git merge equipo/friends

# 3. Trabajar, compilar (Ctrl+Shift+B) y probar. Luego guardar:
git add Vivero.Web/Areas/Tareas/Controllers/RegistrarTareaController.cs Vivero.Web/Areas/Tareas/Views/RegistrarTarea/
git commit -m "UC4.1: formulario de registro de tarea y validaciones"
git push -u origin cu/uc4.1-registrar-tarea

# 4. Cuando tu CU funcione: abrir un Pull Request en GitHub
#    de  cu/uc4.1-registrar-tarea  →  equipo/friends
```
- Usa `git add <tus archivos>` y **no** `git add .` (así no subes por error archivos compartidos).
- Antes de `git commit`, revisa con `git status` que solo aparezcan archivos de tu CU.
- Mensaje de commit: empieza con el código del CU (`UC4.1: …`, `CU05: …`).

### 4.5 Integración (la hace un responsable por equipo)
1. Revisar los PR de su equipo → merge a `equipo/<nombre>`.
2. Compilar y probar todo el módulo.
3. PR de `equipo/<nombre>` → `develop`.
4. La encargada prueba `develop` con todos los módulos juntos y, al final, merge a `main`.

---

## 5. Convenciones de código (iguales para todos)

- Código **explícito**: tipos completos (`List<Tarea> tareas = ...`), nombres en español, comentarios con el paso del CU.
- Cada **extensión** del caso de uso = una validación + un mensaje. Ejemplo (CU01, 6.a):
  ```csharp
  bool ciDuplicado = await _context.Trabajadores.AnyAsync(t => t.CI == modelo.CI);
  if (ciDuplicado)
  {
      ModelState.AddModelError("CI", "Ya existe un trabajador con ese CI. Verifica los datos.");
      return View(modelo);
  }
  ```
- Mensajes después de guardar: `TempData["Exito"] = "Tarea registrada correctamente.";` o `TempData["Error"] = "...";`
  (el `_Layout` ya los muestra).
- **Operaciones "atómicas (todo o nada)"** (tareas + reservas, paquetes + reservas, ventas + stock) → transacción:
  ```csharp
  using Microsoft.EntityFrameworkCore.Storage;

  using (IDbContextTransaction transaccion = await _context.Database.BeginTransactionAsync())
  {
      try
      {
          _context.Tareas.Add(tarea);
          await _context.SaveChangesAsync();
          // ... reservar materiales, historial, notificación ...
          await _context.SaveChangesAsync();
          await transaccion.CommitAsync();
      }
      catch (Exception)
      {
          await transaccion.RollbackAsync();
          TempData["Error"] = "Ocurrió un error. No se guardó ningún cambio.";
          return RedirectToAction("Index");
      }
  }
  ```
- **"Otro usuario modificó el registro"** (UC4.3 9.a, UC6.6 9.a): `Tarea` y `Paquete` tienen columna `Version`.
  Al guardar, haz `tarea.Version = tarea.Version + 1;`. Si otro la cambió antes, EF lanza
  `DbUpdateConcurrencyException` → mostrar "Recarga la información".
- **Bajas lógicas**: personal y plantas **no se borran**, se cambia el estado (`Estado = "Inactivo"`, `EstadoRegistro = "De baja"`).
- **Usuario logueado**: `int idUsuario = UsuarioActual.ObtenerIdUsuario(User);`
- **Notificar**: inyecta `NotificacionService` y usa `NotificarTrabajador(...)` / `NotificarCliente(...)` antes del `SaveChangesAsync`.
- Valores de estado: usa **exactamente** los textos de la BD (la BD rechaza otros):

| Tabla | Estados válidos |
|---|---|
| Sucursal.Estado | Activa, Inactiva |
| Proveedor.Estado | Activo, Inactivo |
| Compra.Estado | Registrada, Pendiente, Cerrada, Cancelada |
| MovimientoStock.Tipo | Compra, Consumo, Ajuste, Baja por cierre de sucursal |
| Trabajador.Estado | Activo, Inactivo |
| Planta.EstadoPlanta | Sana, En crecimiento, Enferma, En tratamiento, Lista para venta |
| Planta.EstadoRegistro | Activo, De baja |
| Tarea.Estado | Pendiente de asignación, Pendiente, En Trabajo, Completado, Cancelada |
| Tarea.Prioridad | Alta, Media, Baja |
| Paquete.Estado | Disponible, Cotizado, Aceptado, En ejecución, Finalizado |
| AsignacionPaquete.Estado | Cotizado, Aceptado, Rechazado |
| ReservaMaterial.Estado | Activa, Consumida, Liberada |
| OrdenCompra.Estado | Pendiente, Aprobada, Comprada, Cancelada |
| Pedido.Estado | Pendiente, Aprobado, Rechazado, Cancelado, En camino, Entregado |
| Entrega.Estado | Pendiente, Asignada, En camino, Entregada, Con incidencia, Recibida, Cancelada |

- **Material disponible en una sucursal** = `StockMaterial.Cantidad - StockMaterial.CantidadReservada`
  (busca la fila con ese `IdMaterial` e `IdSucursal`).
  - **Reservar** (FRIENDS): suma a `CantidadReservada` y crea una fila en `ReservaMaterial` (con `IdSucursal`).
  - **Liberar**: resta de `CantidadReservada` y pone la reserva en `'Liberada'`.
  - **Consumir** (tarea completada): resta de `Cantidad` y de `CantidadReservada`, reserva en `'Consumida'` y registra un
    `MovimientoStock` de tipo `'Consumo'`.
  - **Comprar** (Mapache UC8): suma a `Cantidad` y registra un `MovimientoStock` de tipo `'Compra'`. Si no existe la fila
    de ese material en esa sucursal, se crea.
  - Las tareas de un paquete **reutilizan** las reservas del paquete (`ReservaMaterial.IdPaquete`), no reservan otra vez.

---

## 6. Regla de oro de los casos de uso

**Todo lo que dice el caso de uso tiene que estar en el código**: cada paso del escenario principal
y cada extensión (x.a, x.b…). Antes de hacer tu PR, revisa tu especificación línea por línea y marca:
- [ ] Escenario principal completo
- [ ] Cada extensión tiene su validación y su mensaje
- [ ] Solo pueden entrar los roles del caso de uso (`[Authorize(Roles = ...)]`, ya puesto)
- [ ] Requisitos especiales (historial, bitácora, atómico, no negativos…)

---

## 7. Calendario sugerido (sprints hasta el 4/12)

| Sprint | Fechas | Meta |
|---|---|---|
| Sprint 0 | 19 – 21 nov | Épicas, historias de usuario, backlog, criterios de aceptación, Trello. Todos clonan y ejecutan el proyecto. |
| Sprint 1 | 22 – 27 nov | Escenario principal de cada CU funcionando (listar + registrar/actualizar). PR a la rama del equipo. |
| Sprint 2 | 28 nov – 2 dic | Extensiones, validaciones, transacciones, notificaciones. Merge de cada equipo a `develop`. |
| Integración | 3 dic | Pruebas con todos los módulos juntos, corrección de errores, merge a `main`. |
| Presentación | **4 dic** | Defensa. |
