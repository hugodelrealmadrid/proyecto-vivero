-- =====================================================================
--  SISTEMA DE GESTIÓN PARA VIVERO - Base de datos MySQL 8
--  Proyecto integrado de los equipos: Code³, FRIENDS, Dinamita y Mapache
--
--  REGLA DE ORO: este archivo es la ÚNICA fuente de verdad de la BD.
--  Si tu equipo necesita una columna o tabla nueva, NO la agregues tú:
--  pídela a la encargada de la base (ver GUIA_EQUIPOS.md) para que
--  se agregue aquí y en Data/ViveroDbContext.cs al mismo tiempo.
--
--  Cómo cargarla:  mysql -u root -p < database/vivero_db.sql
--  (o en MySQL Workbench: File > Open SQL Script > Ejecutar el rayo)
-- =====================================================================

DROP DATABASE IF EXISTS vivero_db;
CREATE DATABASE vivero_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE vivero_db;

-- =====================================================================
--  1. BASE COMÚN (la usan todos los equipos)
-- =====================================================================

-- Sucursal: Mapache la registra, edita y da de baja (UC1-UC3, UC5). La usan todos.
-- Quién la creó / dio de baja se guarda en la tabla Bitacora (Entidad = 'Sucursal').
CREATE TABLE Sucursal (
    IdSucursal      INT AUTO_INCREMENT PRIMARY KEY,
    Codigo          VARCHAR(10)  NOT NULL UNIQUE,      -- SUC-001, SUC-002... (lo genera el sistema)
    Nombre          VARCHAR(100) NOT NULL UNIQUE,      -- no se repite (UC1 4a)
    Direccion       VARCHAR(200) NOT NULL,
    Telefono        VARCHAR(20)  NULL,
    Estado          VARCHAR(10)  NOT NULL DEFAULT 'Activa',   -- Activa | Inactiva
    FechaRegistro   DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FechaBaja       DATETIME     NULL,
    CONSTRAINT CK_Sucursal_Estado CHECK (Estado IN ('Activa','Inactiva'))
);

CREATE TABLE Rol (
    IdRol           INT AUTO_INCREMENT PRIMARY KEY,
    Nombre          VARCHAR(30)  NOT NULL UNIQUE,
    Descripcion     VARCHAR(200) NULL
);

CREATE TABLE Puesto (
    IdPuesto        INT AUTO_INCREMENT PRIMARY KEY,
    Nombre          VARCHAR(60)  NOT NULL UNIQUE,
    Descripcion     VARCHAR(200) NULL,
    Activo          BOOLEAN      NOT NULL DEFAULT TRUE
);

CREATE TABLE Especialidad (
    IdEspecialidad  INT AUTO_INCREMENT PRIMARY KEY,
    Nombre          VARCHAR(60)  NOT NULL UNIQUE
);

-- Trabajador (= "Empleado" de Mapache): Code³ lo registra (CU01-CU03);
-- Mapache lo asigna a una sucursal (UC4); FRIENDS lo usa para asignar tareas (UC4.5);
-- Dinamita lo usa como Supervisor en ventas/pedidos.
CREATE TABLE Trabajador (
    IdTrabajador        INT AUTO_INCREMENT PRIMARY KEY,
    CI                  VARCHAR(20)  NOT NULL UNIQUE,
    Nombres             VARCHAR(80)  NOT NULL,
    Apellidos           VARCHAR(80)  NOT NULL,
    Telefono            VARCHAR(20)  NULL,
    Correo              VARCHAR(100) NULL,
    Direccion           VARCHAR(200) NULL,
    FechaIngreso        DATE         NOT NULL,
    IdPuesto            INT          NOT NULL,
    IdSucursal          INT          NULL,      -- NULL = sin sucursal asignada (la asigna Mapache UC4)
    IdEspecialidad      INT          NULL,
    DisponibleReasignacion BOOLEAN   NOT NULL DEFAULT FALSE,   -- Mapache UC4 2a
    Estado              VARCHAR(20)  NOT NULL DEFAULT 'Activo',   -- Activo | Inactivo
    FechaBaja           DATETIME     NULL,
    MotivoBaja          VARCHAR(250) NULL,
    FechaRegistro       DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FechaActualizacion  DATETIME     NULL,
    CONSTRAINT FK_Trabajador_Puesto       FOREIGN KEY (IdPuesto)       REFERENCES Puesto(IdPuesto),
    CONSTRAINT FK_Trabajador_Sucursal     FOREIGN KEY (IdSucursal)     REFERENCES Sucursal(IdSucursal),
    CONSTRAINT FK_Trabajador_Especialidad FOREIGN KEY (IdEspecialidad) REFERENCES Especialidad(IdEspecialidad),
    CONSTRAINT CK_Trabajador_Estado CHECK (Estado IN ('Activo','Inactivo'))
);

-- Cliente: Dinamita lo registra/actualiza (CU12, CU13); FRIENDS lo usa en paquetes.
CREATE TABLE Cliente (
    IdCliente       INT AUTO_INCREMENT PRIMARY KEY,
    CiNit           VARCHAR(20)  NOT NULL UNIQUE,
    Nombre          VARCHAR(120) NOT NULL,
    Telefono        VARCHAR(20)  NULL,
    Correo          VARCHAR(100) NULL,
    Direccion       VARCHAR(200) NULL,
    FechaRegistro   DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Activo          BOOLEAN      NOT NULL DEFAULT TRUE
);

-- Usuario del sistema (login). Puede estar ligado a un Trabajador o a un Cliente.
CREATE TABLE Usuario (
    IdUsuario       INT AUTO_INCREMENT PRIMARY KEY,
    NombreUsuario   VARCHAR(50)  NOT NULL UNIQUE,
    Correo          VARCHAR(100) NULL,
    PasswordHash    VARCHAR(255) NOT NULL,
    IdRol           INT          NOT NULL,
    IdTrabajador    INT          NULL,
    IdCliente       INT          NULL,
    Activo          BOOLEAN      NOT NULL DEFAULT TRUE,
    FechaCreacion   DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT FK_Usuario_Rol        FOREIGN KEY (IdRol)        REFERENCES Rol(IdRol),
    CONSTRAINT FK_Usuario_Trabajador FOREIGN KEY (IdTrabajador) REFERENCES Trabajador(IdTrabajador),
    CONSTRAINT FK_Usuario_Cliente    FOREIGN KEY (IdCliente)    REFERENCES Cliente(IdCliente)
);

-- Planta (= "Producto" en el diagrama de Dinamita). Code³ la registra (CU04-CU07),
-- Dinamita la vende, FRIENDS la usa dentro de los paquetes.
CREATE TABLE Planta (
    IdPlanta            INT AUTO_INCREMENT PRIMARY KEY,
    Codigo              VARCHAR(20)   NOT NULL UNIQUE,
    Nombre              VARCHAR(100)  NOT NULL,
    Especie             VARCHAR(100)  NULL,
    Tipo                VARCHAR(60)   NULL,        -- Ornamental, Frutal, Suculenta, ...
    Descripcion         VARCHAR(500)  NULL,
    EsLote              BOOLEAN       NOT NULL DEFAULT FALSE,
    Cantidad            INT           NOT NULL DEFAULT 0,   -- stock disponible
    EstadoPlanta        VARCHAR(30)   NOT NULL DEFAULT 'Sana',
    PrecioVenta         DECIMAL(10,2) NOT NULL DEFAULT 0,
    CostoReferencial    DECIMAL(10,2) NOT NULL DEFAULT 0,
    ImagenUrl           VARCHAR(300)  NULL,
    IdSucursal          INT           NOT NULL,
    EstadoRegistro      VARCHAR(20)   NOT NULL DEFAULT 'Activo', -- Activo | De baja
    FechaBaja           DATETIME      NULL,
    MotivoBaja          VARCHAR(250)  NULL,
    FechaRegistro       DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT FK_Planta_Sucursal FOREIGN KEY (IdSucursal) REFERENCES Sucursal(IdSucursal),
    CONSTRAINT CK_Planta_Cantidad CHECK (Cantidad >= 0),
    CONSTRAINT CK_Planta_Estado   CHECK (EstadoPlanta IN ('Sana','En crecimiento','Enferma','En tratamiento','Lista para venta')),
    CONSTRAINT CK_Planta_Registro CHECK (EstadoRegistro IN ('Activo','De baja'))
);

-- Material / suministro / insumo (abono, macetas, tierra, ...): solo el CATÁLOGO.
-- La cantidad que hay en cada sucursal está en StockMaterial (módulo de Mapache).
-- Mapache los compra (UC8); Code³ los pide en órdenes de compra (CU08);
-- FRIENDS los reserva en tareas y paquetes.
CREATE TABLE Material (
    IdMaterial      INT AUTO_INCREMENT PRIMARY KEY,
    Nombre          VARCHAR(100)  NOT NULL UNIQUE,
    Tipo            VARCHAR(60)   NULL,              -- Sustrato, Abono, Maceta, Decoración...
    Unidad          VARCHAR(20)   NOT NULL,          -- kg, unidad, bolsa, litro... (unidadMedida)
    CostoUnitario   DECIMAL(10,2) NOT NULL DEFAULT 0, -- costo de referencia
    Activo          BOOLEAN       NOT NULL DEFAULT TRUE
);

-- Servicio que puede ir dentro de un paquete (instalación, diseño, transporte...)
CREATE TABLE Servicio (
    IdServicio          INT AUTO_INCREMENT PRIMARY KEY,
    Nombre              VARCHAR(100)  NOT NULL UNIQUE,
    Descripcion         VARCHAR(300)  NULL,
    CostoReferencial    DECIMAL(10,2) NOT NULL DEFAULT 0,
    Activo              BOOLEAN       NOT NULL DEFAULT TRUE
);

-- Notificaciones internas (tareas asignadas, cotizaciones, estados de entrega...)
CREATE TABLE Notificacion (
    IdNotificacion      INT AUTO_INCREMENT PRIMARY KEY,
    IdUsuarioDestino    INT           NOT NULL,
    Titulo              VARCHAR(120)  NOT NULL,
    Mensaje             VARCHAR(500)  NOT NULL,
    Leida               BOOLEAN       NOT NULL DEFAULT FALSE,
    Fecha               DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT FK_Notificacion_Usuario FOREIGN KEY (IdUsuarioDestino) REFERENCES Usuario(IdUsuario)
);

-- Bitácora general (eliminaciones, cambios importantes, auditoría)
CREATE TABLE Bitacora (
    IdBitacora      INT AUTO_INCREMENT PRIMARY KEY,
    Entidad         VARCHAR(50)   NOT NULL,   -- 'Tarea', 'Paquete', 'Trabajador', ...
    IdRegistro      INT           NOT NULL,
    Accion          VARCHAR(50)   NOT NULL,   -- 'Eliminar', 'Modificar', 'Baja', ...
    Detalle         TEXT          NULL,
    IdUsuario       INT           NULL,
    Fecha           DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT FK_Bitacora_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
);

-- =====================================================================
--  2. EQUIPO MAPACHE - Gestión de material y proveedores
--     (Sucursal está arriba, en la base común)
-- =====================================================================

-- UC6, UC7, UC10: catálogo de proveedores compartido por todas las sucursales
CREATE TABLE Proveedor (
    IdProveedor         INT AUTO_INCREMENT PRIMARY KEY,
    Nombre              VARCHAR(120) NOT NULL,        -- nombre o empresa
    TipoMaterial        VARCHAR(100) NOT NULL,        -- tipo de material que provee
    Contacto            VARCHAR(120) NOT NULL,        -- persona / teléfono / correo
    Estado              VARCHAR(10)  NOT NULL DEFAULT 'Activo',   -- Activo | Inactivo
    IdUsuarioRegistro   INT          NOT NULL,        -- solo él o el Administrador lo editan
    FechaRegistro       DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FechaBaja           DATETIME     NULL,
    CONSTRAINT FK_Proveedor_Usuario FOREIGN KEY (IdUsuarioRegistro) REFERENCES Usuario(IdUsuario),
    CONSTRAINT UQ_Proveedor UNIQUE (Nombre, Contacto),     -- evita duplicados
    CONSTRAINT CK_Proveedor_Estado CHECK (Estado IN ('Activo','Inactivo'))
);

-- UC9, UC12: stock de cada material EN CADA SUCURSAL, con su umbral mínimo.
-- Disponible para usar = Cantidad - CantidadReservada
CREATE TABLE StockMaterial (
    IdStockMaterial     INT AUTO_INCREMENT PRIMARY KEY,
    IdMaterial          INT           NOT NULL,
    IdSucursal          INT           NOT NULL,
    Cantidad            DECIMAL(10,2) NOT NULL DEFAULT 0,
    CantidadReservada   DECIMAL(10,2) NOT NULL DEFAULT 0,   -- lo reservan tareas y paquetes (FRIENDS)
    UmbralMinimo        DECIMAL(10,2) NULL,                 -- NULL = sin configurar (UC11 1a)
    CONSTRAINT FK_Stock_Material FOREIGN KEY (IdMaterial) REFERENCES Material(IdMaterial),
    CONSTRAINT FK_Stock_Sucursal FOREIGN KEY (IdSucursal) REFERENCES Sucursal(IdSucursal),
    CONSTRAINT UQ_Stock UNIQUE (IdMaterial, IdSucursal),
    CONSTRAINT CK_Stock_Cantidad CHECK (Cantidad >= 0 AND CantidadReservada >= 0),
    CONSTRAINT CK_Stock_Umbral CHECK (UmbralMinimo IS NULL OR UmbralMinimo >= 0)
);

-- Cada entrada o salida de stock (para saber por qué cambió: compra, consumo, baja por cierre...)
CREATE TABLE MovimientoStock (
    IdMovimiento        INT AUTO_INCREMENT PRIMARY KEY,
    IdStockMaterial     INT           NOT NULL,
    Tipo                VARCHAR(30)   NOT NULL,
    Cantidad            DECIMAL(10,2) NOT NULL,    -- positiva = entra, negativa = sale
    Referencia          VARCHAR(100)  NULL,        -- 'Compra C-0001', 'Tarea #5', ...
    IdUsuario           INT           NULL,
    Fecha               DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT FK_Movimiento_Stock   FOREIGN KEY (IdStockMaterial) REFERENCES StockMaterial(IdStockMaterial),
    CONSTRAINT FK_Movimiento_Usuario FOREIGN KEY (IdUsuario)       REFERENCES Usuario(IdUsuario),
    CONSTRAINT CK_Movimiento_Tipo CHECK (Tipo IN ('Compra','Consumo','Ajuste','Baja por cierre de sucursal'))
);

-- UC11: alertas de bajo stock
CREATE TABLE AlertaStock (
    IdAlerta            INT AUTO_INCREMENT PRIMARY KEY,
    IdStockMaterial     INT          NOT NULL,
    Fecha               DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Mensaje             VARCHAR(300) NOT NULL,
    Atendida            BOOLEAN      NOT NULL DEFAULT FALSE,
    CONSTRAINT FK_Alerta_Stock FOREIGN KEY (IdStockMaterial) REFERENCES StockMaterial(IdStockMaterial)
);

-- =====================================================================
--  3. EQUIPO CODE³ - Gestión de Personal y Gestión de Plantas
-- =====================================================================

-- CU05: historial de cambios de cantidad / estado de una planta
CREATE TABLE HistorialPlanta (
    IdHistorialPlanta   INT AUTO_INCREMENT PRIMARY KEY,
    IdPlanta            INT          NOT NULL,
    CantidadAnterior    INT          NOT NULL,
    CantidadNueva       INT          NOT NULL,
    EstadoAnterior      VARCHAR(30)  NOT NULL,
    EstadoNuevo         VARCHAR(30)  NOT NULL,
    Motivo              VARCHAR(250) NULL,     -- ingreso, pérdida, trasplante...
    IdUsuario           INT          NOT NULL,
    Fecha               DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT FK_HistorialPlanta_Planta  FOREIGN KEY (IdPlanta)  REFERENCES Planta(IdPlanta),
    CONSTRAINT FK_HistorialPlanta_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
);

-- CU06: tipos de actividad de cuidado
CREATE TABLE TipoActividad (
    IdTipoActividad INT AUTO_INCREMENT PRIMARY KEY,
    Nombre          VARCHAR(60) NOT NULL UNIQUE
);

-- CU06: actividades realizadas o reportes de mantenimiento
CREATE TABLE ActividadPlanta (
    IdActividad         INT AUTO_INCREMENT PRIMARY KEY,
    IdPlanta            INT          NOT NULL,
    IdTipoActividad     INT          NOT NULL,
    Clase               VARCHAR(20)  NOT NULL DEFAULT 'Actividad', -- Actividad | Reporte
    FechaActividad      DATE         NOT NULL,
    IdTrabajador        INT          NOT NULL,   -- responsable
    Observaciones       VARCHAR(500) NULL,
    ProximaFecha        DATE         NULL,       -- próximo mantenimiento (opcional)
    IdUsuarioRegistro   INT          NOT NULL,
    FechaRegistro       DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT FK_Actividad_Planta     FOREIGN KEY (IdPlanta)          REFERENCES Planta(IdPlanta),
    CONSTRAINT FK_Actividad_Tipo       FOREIGN KEY (IdTipoActividad)   REFERENCES TipoActividad(IdTipoActividad),
    CONSTRAINT FK_Actividad_Trabajador FOREIGN KEY (IdTrabajador)      REFERENCES Trabajador(IdTrabajador),
    CONSTRAINT FK_Actividad_Usuario    FOREIGN KEY (IdUsuarioRegistro) REFERENCES Usuario(IdUsuario),
    CONSTRAINT CK_Actividad_Clase CHECK (Clase IN ('Actividad','Reporte'))
);

-- CU08: orden de compra de suministros = el PEDIDO/solicitud (todavía no mueve stock).
-- Cuando se compra de verdad, Mapache registra la Compra (UC8) ligada a esta orden
-- y la orden pasa a 'Comprada'.
CREATE TABLE OrdenCompra (
    IdOrdenCompra   INT AUTO_INCREMENT PRIMARY KEY,
    Codigo          VARCHAR(20)   NOT NULL UNIQUE,
    FechaGeneracion DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    IdUsuario       INT           NOT NULL,     -- responsable
    IdSucursal      INT           NOT NULL,     -- sucursal que necesita los suministros
    IdProveedor     INT           NULL,         -- proveedor sugerido (opcional)
    Estado          VARCHAR(20)   NOT NULL DEFAULT 'Pendiente',
    Total           DECIMAL(10,2) NOT NULL DEFAULT 0,
    Observaciones   VARCHAR(300)  NULL,
    CONSTRAINT FK_OrdenCompra_Usuario   FOREIGN KEY (IdUsuario)   REFERENCES Usuario(IdUsuario),
    CONSTRAINT FK_OrdenCompra_Sucursal  FOREIGN KEY (IdSucursal)  REFERENCES Sucursal(IdSucursal),
    CONSTRAINT FK_OrdenCompra_Proveedor FOREIGN KEY (IdProveedor) REFERENCES Proveedor(IdProveedor),
    CONSTRAINT CK_OrdenCompra_Estado CHECK (Estado IN ('Pendiente','Aprobada','Comprada','Cancelada'))
);

CREATE TABLE DetalleOrdenCompra (
    IdDetalleOrdenCompra INT AUTO_INCREMENT PRIMARY KEY,
    IdOrdenCompra        INT           NOT NULL,
    IdMaterial           INT           NOT NULL,
    Cantidad             DECIMAL(10,2) NOT NULL,
    PrecioUnitario       DECIMAL(10,2) NOT NULL DEFAULT 0,
    Subtotal             DECIMAL(10,2) NOT NULL DEFAULT 0,
    CONSTRAINT FK_DetalleOC_Orden    FOREIGN KEY (IdOrdenCompra) REFERENCES OrdenCompra(IdOrdenCompra),
    CONSTRAINT FK_DetalleOC_Material FOREIGN KEY (IdMaterial)    REFERENCES Material(IdMaterial),
    CONSTRAINT CK_DetalleOC_Cantidad CHECK (Cantidad > 0)
);

-- MAPACHE UC8: compra REAL de material a un proveedor (actualiza StockMaterial y genera comprobante)
CREATE TABLE Compra (
    IdCompra        INT AUTO_INCREMENT PRIMARY KEY,
    Comprobante     VARCHAR(20)   NOT NULL UNIQUE,   -- C-0001, C-0002...
    FechaCompra     DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    IdProveedor     INT           NOT NULL,
    IdSucursal      INT           NOT NULL,
    IdUsuario       INT           NOT NULL,          -- auditoría: quién la registró
    IdOrdenCompra   INT           NULL,              -- si viene de una orden de Code³ (CU08)
    Total           DECIMAL(10,2) NOT NULL DEFAULT 0,
    Estado          VARCHAR(15)   NOT NULL DEFAULT 'Registrada',
    CONSTRAINT FK_Compra_Proveedor FOREIGN KEY (IdProveedor)   REFERENCES Proveedor(IdProveedor),
    CONSTRAINT FK_Compra_Sucursal  FOREIGN KEY (IdSucursal)    REFERENCES Sucursal(IdSucursal),
    CONSTRAINT FK_Compra_Usuario   FOREIGN KEY (IdUsuario)     REFERENCES Usuario(IdUsuario),
    CONSTRAINT FK_Compra_Orden     FOREIGN KEY (IdOrdenCompra) REFERENCES OrdenCompra(IdOrdenCompra),
    CONSTRAINT CK_Compra_Estado CHECK (Estado IN ('Registrada','Pendiente','Cerrada','Cancelada'))
);

CREATE TABLE DetalleCompra (
    IdDetalleCompra INT AUTO_INCREMENT PRIMARY KEY,
    IdCompra        INT           NOT NULL,
    IdMaterial      INT           NOT NULL,
    Cantidad        DECIMAL(10,2) NOT NULL,
    PrecioUnitario  DECIMAL(10,2) NOT NULL,
    Subtotal        DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_DetalleCompra_Compra   FOREIGN KEY (IdCompra)   REFERENCES Compra(IdCompra) ON DELETE CASCADE,
    CONSTRAINT FK_DetalleCompra_Material FOREIGN KEY (IdMaterial) REFERENCES Material(IdMaterial),
    CONSTRAINT CK_DetalleCompra_Cantidad CHECK (Cantidad > 0)
);

-- =====================================================================
--  4. EQUIPO FRIENDS - Módulo 6: Paquetes de decoración
--     (va antes que Tareas porque una tarea puede pertenecer a un paquete)
-- =====================================================================

CREATE TABLE Proyecto (
    IdProyecto      INT AUTO_INCREMENT PRIMARY KEY,
    IdCliente       INT          NOT NULL,
    Nombre          VARCHAR(120) NOT NULL,
    Descripcion     VARCHAR(500) NULL,
    Ubicacion       VARCHAR(200) NULL,
    Estado          VARCHAR(20)  NOT NULL DEFAULT 'Abierto',
    FechaRegistro   DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT FK_Proyecto_Cliente FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente),
    CONSTRAINT CK_Proyecto_Estado CHECK (Estado IN ('Abierto','Cerrado','Cancelado'))
);

-- UC6.1 / UC6.2: paquete del catálogo o personalizado
CREATE TABLE Paquete (
    IdPaquete           INT AUTO_INCREMENT PRIMARY KEY,
    Codigo              VARCHAR(20)   NOT NULL UNIQUE,
    Nombre              VARCHAR(120)  NOT NULL,
    TipoEvento          VARCHAR(60)   NOT NULL,    -- Boda, Cumpleaños, Corporativo...
    Estilo              VARCHAR(60)   NULL,        -- Rústico, Moderno, Tropical...
    Descripcion         VARCHAR(1000) NULL,
    EsPersonalizado     BOOLEAN       NOT NULL DEFAULT FALSE,
    IdCliente           INT           NULL,        -- solo si es personalizado
    IdSucursal          INT           NULL,        -- sucursal de donde salen los materiales reservados
    Ubicacion           VARCHAR(200)  NULL,
    FechaPrevista       DATE          NULL,
    Necesidades         VARCHAR(1000) NULL,
    CostoReferencial    DECIMAL(10,2) NOT NULL DEFAULT 0,  -- el Cliente NUNCA lo ve
    Precio              DECIMAL(10,2) NOT NULL,
    Estado              VARCHAR(20)   NOT NULL DEFAULT 'Disponible',
    IdUsuarioCreador    INT           NOT NULL,
    FechaCreacion       DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FechaModificacion   DATETIME      NULL,
    Version             INT           NOT NULL DEFAULT 1,  -- control de concurrencia
    CONSTRAINT FK_Paquete_Cliente FOREIGN KEY (IdCliente)        REFERENCES Cliente(IdCliente),
    CONSTRAINT FK_Paquete_Sucursal FOREIGN KEY (IdSucursal)      REFERENCES Sucursal(IdSucursal),
    CONSTRAINT FK_Paquete_Usuario FOREIGN KEY (IdUsuarioCreador) REFERENCES Usuario(IdUsuario),
    CONSTRAINT CK_Paquete_Precio CHECK (Precio > 0),
    CONSTRAINT CK_Paquete_Estado CHECK (Estado IN ('Disponible','Cotizado','Aceptado','En ejecución','Finalizado'))
);

CREATE TABLE PaqueteImagen (
    IdPaqueteImagen INT AUTO_INCREMENT PRIMARY KEY,
    IdPaquete       INT          NOT NULL,
    Url             VARCHAR(300) NOT NULL,
    CONSTRAINT FK_PaqueteImagen_Paquete FOREIGN KEY (IdPaquete) REFERENCES Paquete(IdPaquete) ON DELETE CASCADE
);

-- Elementos del paquete: plantas, materiales o servicios
CREATE TABLE PaqueteDetalle (
    IdPaqueteDetalle INT AUTO_INCREMENT PRIMARY KEY,
    IdPaquete        INT           NOT NULL,
    TipoElemento     VARCHAR(20)   NOT NULL,   -- Planta | Material | Servicio
    IdPlanta         INT           NULL,
    IdMaterial       INT           NULL,
    IdServicio       INT           NULL,
    Cantidad         DECIMAL(10,2) NOT NULL,
    CostoUnitario    DECIMAL(10,2) NOT NULL,
    Subtotal         DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_PaqueteDetalle_Paquete  FOREIGN KEY (IdPaquete)  REFERENCES Paquete(IdPaquete) ON DELETE CASCADE,
    CONSTRAINT FK_PaqueteDetalle_Planta   FOREIGN KEY (IdPlanta)   REFERENCES Planta(IdPlanta),
    CONSTRAINT FK_PaqueteDetalle_Material FOREIGN KEY (IdMaterial) REFERENCES Material(IdMaterial),
    CONSTRAINT FK_PaqueteDetalle_Servicio FOREIGN KEY (IdServicio) REFERENCES Servicio(IdServicio),
    CONSTRAINT CK_PaqueteDetalle_Tipo     CHECK (TipoElemento IN ('Planta','Material','Servicio')),
    CONSTRAINT CK_PaqueteDetalle_Cantidad CHECK (Cantidad > 0)
);

-- UC6.6: vínculo paquete - cliente - proyecto
CREATE TABLE AsignacionPaquete (
    IdAsignacion         INT AUTO_INCREMENT PRIMARY KEY,
    IdPaquete            INT           NOT NULL,
    IdCliente            INT           NOT NULL,
    IdProyecto           INT           NULL,
    FechaServicio        DATE          NOT NULL,
    Ubicacion            VARCHAR(200)  NOT NULL,
    Condiciones          VARCHAR(1000) NULL,
    PrecioCotizado       DECIMAL(10,2) NOT NULL,
    Estado               VARCHAR(20)   NOT NULL DEFAULT 'Cotizado', -- Cotizado | Aceptado | Rechazado
    IdUsuario            INT           NOT NULL,
    FechaAsignacion      DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FechaAceptacion      DATETIME      NULL,
    IdUsuarioAceptacion  INT           NULL,
    EvidenciaAceptacion  VARCHAR(300)  NULL,
    CONSTRAINT FK_Asignacion_Paquete  FOREIGN KEY (IdPaquete)  REFERENCES Paquete(IdPaquete),
    CONSTRAINT FK_Asignacion_Cliente  FOREIGN KEY (IdCliente)  REFERENCES Cliente(IdCliente),
    CONSTRAINT FK_Asignacion_Proyecto FOREIGN KEY (IdProyecto) REFERENCES Proyecto(IdProyecto),
    CONSTRAINT FK_Asignacion_Usuario  FOREIGN KEY (IdUsuario)  REFERENCES Usuario(IdUsuario),
    CONSTRAINT FK_Asignacion_UsuarioA FOREIGN KEY (IdUsuarioAceptacion) REFERENCES Usuario(IdUsuario),
    CONSTRAINT UQ_Asignacion UNIQUE (IdPaquete, IdCliente),
    CONSTRAINT CK_Asignacion_Estado CHECK (Estado IN ('Cotizado','Aceptado','Rechazado'))
);

-- Cotizaciones enviadas al cliente (una nueva versión cada vez que se modifica el paquete, UC6.4)
CREATE TABLE Cotizacion (
    IdCotizacion    INT AUTO_INCREMENT PRIMARY KEY,
    IdPaquete       INT           NOT NULL,
    IdCliente       INT           NOT NULL,
    NumeroVersion   INT           NOT NULL DEFAULT 1,
    Precio          DECIMAL(10,2) NOT NULL,
    Detalle         TEXT          NULL,
    Estado          VARCHAR(20)   NOT NULL DEFAULT 'Vigente', -- Vigente | Reemplazada | Aprobada
    Fecha           DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT FK_Cotizacion_Paquete FOREIGN KEY (IdPaquete) REFERENCES Paquete(IdPaquete),
    CONSTRAINT FK_Cotizacion_Cliente FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente)
);

-- =====================================================================
--  5. EQUIPO FRIENDS - Módulo 4: Asignación de tareas
-- =====================================================================

CREATE TABLE Tarea (
    IdTarea                 INT AUTO_INCREMENT PRIMARY KEY,
    TipoTrabajo             VARCHAR(60)   NOT NULL,
    Descripcion             VARCHAR(500)  NOT NULL,
    Ubicacion               VARCHAR(200)  NOT NULL,
    FechaProgramada         DATE          NOT NULL,
    Prioridad               VARCHAR(10)   NOT NULL DEFAULT 'Media',
    Estado                  VARCHAR(30)   NOT NULL DEFAULT 'Pendiente',
    IdTrabajador            INT           NULL,
    IdEspecialidadRequerida INT           NULL,
    IdSucursal              INT           NOT NULL,
    IdPaquete               INT           NULL,       -- paquete asociado (opcional)
    FechaInicio             DATETIME      NULL,
    FechaFin                DATETIME      NULL,
    IdUsuarioCreador        INT           NOT NULL,
    FechaRegistro           DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Version                 INT           NOT NULL DEFAULT 1,  -- control de concurrencia (UC4.3 9.a)
    CONSTRAINT FK_Tarea_Trabajador   FOREIGN KEY (IdTrabajador)            REFERENCES Trabajador(IdTrabajador),
    CONSTRAINT FK_Tarea_Especialidad FOREIGN KEY (IdEspecialidadRequerida) REFERENCES Especialidad(IdEspecialidad),
    CONSTRAINT FK_Tarea_Sucursal     FOREIGN KEY (IdSucursal)              REFERENCES Sucursal(IdSucursal),
    CONSTRAINT FK_Tarea_Paquete      FOREIGN KEY (IdPaquete)               REFERENCES Paquete(IdPaquete),
    CONSTRAINT FK_Tarea_Usuario      FOREIGN KEY (IdUsuarioCreador)        REFERENCES Usuario(IdUsuario),
    CONSTRAINT CK_Tarea_Prioridad CHECK (Prioridad IN ('Alta','Media','Baja')),
    CONSTRAINT CK_Tarea_Estado    CHECK (Estado IN ('Pendiente de asignación','Pendiente','En Trabajo','Completado','Cancelada'))
);

-- Materiales que necesita una tarea
CREATE TABLE TareaMaterial (
    IdTareaMaterial     INT AUTO_INCREMENT PRIMARY KEY,
    IdTarea             INT           NOT NULL,
    IdMaterial          INT           NOT NULL,
    CantidadRequerida   DECIMAL(10,2) NOT NULL,
    CantidadUsada       DECIMAL(10,2) NOT NULL DEFAULT 0,
    CONSTRAINT FK_TareaMaterial_Tarea    FOREIGN KEY (IdTarea)    REFERENCES Tarea(IdTarea) ON DELETE CASCADE,
    CONSTRAINT FK_TareaMaterial_Material FOREIGN KEY (IdMaterial) REFERENCES Material(IdMaterial),
    CONSTRAINT CK_TareaMaterial_Cantidad CHECK (CantidadRequerida > 0)
);

-- Historial de cambios de estado de la tarea (UC4.2 lo muestra, UC4.3 y UC4.4 lo escriben)
CREATE TABLE HistorialTarea (
    IdHistorialTarea INT AUTO_INCREMENT PRIMARY KEY,
    IdTarea          INT          NOT NULL,
    Accion           VARCHAR(40)  NOT NULL,    -- Registro, Modificación, Reasignación, Cambio de estado, Cancelación
    EstadoAnterior   VARCHAR(30)  NULL,
    EstadoNuevo      VARCHAR(30)  NULL,
    Motivo           VARCHAR(300) NULL,
    ValoresAnteriores TEXT        NULL,
    ValoresNuevos    TEXT         NULL,
    IdUsuario        INT          NOT NULL,
    Fecha            DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT FK_HistorialTarea_Tarea   FOREIGN KEY (IdTarea)   REFERENCES Tarea(IdTarea) ON DELETE CASCADE,
    CONSTRAINT FK_HistorialTarea_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
);

-- Reservas de material (compartida por paquetes y tareas, para NO reservar dos veces).
-- Al reservar se suma a StockMaterial.CantidadReservada de ESA sucursal.
CREATE TABLE ReservaMaterial (
    IdReserva       INT AUTO_INCREMENT PRIMARY KEY,
    IdMaterial      INT           NOT NULL,
    IdSucursal      INT           NOT NULL,
    Cantidad        DECIMAL(10,2) NOT NULL,
    Origen          VARCHAR(20)   NOT NULL,    -- Paquete | Tarea
    IdPaquete       INT           NULL,
    IdTarea         INT           NULL,
    Estado          VARCHAR(20)   NOT NULL DEFAULT 'Activa',  -- Activa | Consumida | Liberada
    Fecha           DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT FK_Reserva_Material FOREIGN KEY (IdMaterial) REFERENCES Material(IdMaterial),
    CONSTRAINT FK_Reserva_Sucursal FOREIGN KEY (IdSucursal) REFERENCES Sucursal(IdSucursal),
    CONSTRAINT FK_Reserva_Paquete  FOREIGN KEY (IdPaquete)  REFERENCES Paquete(IdPaquete),
    CONSTRAINT FK_Reserva_Tarea    FOREIGN KEY (IdTarea)    REFERENCES Tarea(IdTarea) ON DELETE SET NULL,
    CONSTRAINT CK_Reserva_Origen CHECK (Origen IN ('Paquete','Tarea')),
    CONSTRAINT CK_Reserva_Estado CHECK (Estado IN ('Activa','Consumida','Liberada'))
);

-- =====================================================================
--  6. EQUIPO DINAMITA - Ventas y pedidos
-- =====================================================================

-- CU09: venta en vivero. Supervisor = Trabajador que registra la venta.
CREATE TABLE Venta (
    IdVenta         INT AUTO_INCREMENT PRIMARY KEY,
    FechaVenta      DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Total           DECIMAL(10,2) NOT NULL DEFAULT 0,
    IdSucursal      INT           NOT NULL,
    IdCliente       INT           NULL,       -- 0..1 en el diagrama
    IdSupervisor    INT           NOT NULL,   -- Trabajador
    Estado          VARCHAR(20)   NOT NULL DEFAULT 'Registrada',
    CONSTRAINT FK_Venta_Sucursal   FOREIGN KEY (IdSucursal)   REFERENCES Sucursal(IdSucursal),
    CONSTRAINT FK_Venta_Cliente    FOREIGN KEY (IdCliente)    REFERENCES Cliente(IdCliente),
    CONSTRAINT FK_Venta_Supervisor FOREIGN KEY (IdSupervisor) REFERENCES Trabajador(IdTrabajador)
);

CREATE TABLE DetalleVenta (
    IdDetalleVenta  INT AUTO_INCREMENT PRIMARY KEY,
    IdVenta         INT           NOT NULL,
    IdPlanta        INT           NOT NULL,
    Cantidad        INT           NOT NULL,
    PrecioUnitario  DECIMAL(10,2) NOT NULL,
    Subtotal        DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_DetalleVenta_Venta  FOREIGN KEY (IdVenta)  REFERENCES Venta(IdVenta) ON DELETE CASCADE,
    CONSTRAINT FK_DetalleVenta_Planta FOREIGN KEY (IdPlanta) REFERENCES Planta(IdPlanta),
    CONSTRAINT CK_DetalleVenta_Cantidad CHECK (Cantidad > 0)
);

-- CU14-CU18: pedido en línea del cliente
CREATE TABLE Pedido (
    IdPedido            INT AUTO_INCREMENT PRIMARY KEY,
    FechaPedido         DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Estado              VARCHAR(20)   NOT NULL DEFAULT 'Pendiente',
    Total               DECIMAL(10,2) NOT NULL DEFAULT 0,
    IdCliente           INT           NOT NULL,
    IdSucursal          INT           NOT NULL,
    IdSupervisor        INT           NULL,       -- quien aprueba (0..1)
    FechaAprobacion     DATETIME      NULL,
    MotivoCancelacion   VARCHAR(250)  NULL,
    CONSTRAINT FK_Pedido_Cliente    FOREIGN KEY (IdCliente)    REFERENCES Cliente(IdCliente),
    CONSTRAINT FK_Pedido_Sucursal   FOREIGN KEY (IdSucursal)   REFERENCES Sucursal(IdSucursal),
    CONSTRAINT FK_Pedido_Supervisor FOREIGN KEY (IdSupervisor) REFERENCES Trabajador(IdTrabajador),
    CONSTRAINT CK_Pedido_Estado CHECK (Estado IN ('Pendiente','Aprobado','Rechazado','Cancelado','En camino','Entregado'))
);

CREATE TABLE DetallePedido (
    IdDetallePedido INT AUTO_INCREMENT PRIMARY KEY,
    IdPedido        INT           NOT NULL,
    IdPlanta        INT           NOT NULL,
    Cantidad        INT           NOT NULL,
    PrecioUnitario  DECIMAL(10,2) NOT NULL,
    Subtotal        DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_DetallePedido_Pedido FOREIGN KEY (IdPedido) REFERENCES Pedido(IdPedido) ON DELETE CASCADE,
    CONSTRAINT FK_DetallePedido_Planta FOREIGN KEY (IdPlanta) REFERENCES Planta(IdPlanta),
    CONSTRAINT CK_DetallePedido_Cantidad CHECK (Cantidad > 0)
);

-- =====================================================================
--  7. EQUIPO DINAMITA - Gestión de entregas
-- =====================================================================

CREATE TABLE DireccionEntrega (
    IdDireccionEntrega INT AUTO_INCREMENT PRIMARY KEY,
    IdCliente          INT          NOT NULL,
    Direccion          VARCHAR(200) NOT NULL,
    Referencia         VARCHAR(200) NULL,
    CONSTRAINT FK_DireccionEntrega_Cliente FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente)
);

CREATE TABLE Vehiculo (
    IdVehiculo  INT AUTO_INCREMENT PRIMARY KEY,
    Placa       VARCHAR(15) NOT NULL UNIQUE,
    Tipo        VARCHAR(40) NOT NULL,
    Disponible  BOOLEAN     NOT NULL DEFAULT TRUE
);

-- Conductor (puede estar ligado a un Trabajador registrado por Code³)
CREATE TABLE Conductor (
    IdConductor     INT AUTO_INCREMENT PRIMARY KEY,
    Nombre          VARCHAR(120) NOT NULL,
    Telefono        VARCHAR(20)  NULL,
    Licencia        VARCHAR(30)  NULL,
    Disponible      BOOLEAN      NOT NULL DEFAULT TRUE,
    IdTrabajador    INT          NULL UNIQUE,
    CONSTRAINT FK_Conductor_Trabajador FOREIGN KEY (IdTrabajador) REFERENCES Trabajador(IdTrabajador)
);

CREATE TABLE Entrega (
    IdEntrega           INT AUTO_INCREMENT PRIMARY KEY,
    IdPedido            INT          NOT NULL UNIQUE,  -- Pedido 1 : 1 Entrega
    Codigo              VARCHAR(20)  NOT NULL UNIQUE,
    Estado              VARCHAR(20)  NOT NULL DEFAULT 'Pendiente',
    FechaHora           DATETIME     NOT NULL,
    Foto                VARCHAR(300) NULL,
    IdDireccionEntrega  INT          NOT NULL,
    IdVehiculo          INT          NULL,
    IdConductor         INT          NULL,
    CONSTRAINT FK_Entrega_Pedido    FOREIGN KEY (IdPedido)           REFERENCES Pedido(IdPedido),
    CONSTRAINT FK_Entrega_Direccion FOREIGN KEY (IdDireccionEntrega) REFERENCES DireccionEntrega(IdDireccionEntrega),
    CONSTRAINT FK_Entrega_Vehiculo  FOREIGN KEY (IdVehiculo)         REFERENCES Vehiculo(IdVehiculo),
    CONSTRAINT FK_Entrega_Conductor FOREIGN KEY (IdConductor)        REFERENCES Conductor(IdConductor),
    CONSTRAINT CK_Entrega_Estado CHECK (Estado IN ('Pendiente','Asignada','En camino','Entregada','Con incidencia','Recibida','Cancelada'))
);

CREATE TABLE SolicitudConductor (
    IdSolicitud     INT AUTO_INCREMENT PRIMARY KEY,
    IdEntrega       INT         NOT NULL,
    IdConductor     INT         NOT NULL,
    Estado          VARCHAR(20) NOT NULL DEFAULT 'Pendiente', -- Pendiente | Aceptada | Rechazada
    FechaHora       DATETIME    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT FK_Solicitud_Entrega   FOREIGN KEY (IdEntrega)   REFERENCES Entrega(IdEntrega),
    CONSTRAINT FK_Solicitud_Conductor FOREIGN KEY (IdConductor) REFERENCES Conductor(IdConductor)
);

CREATE TABLE Incidencia (
    IdIncidencia    INT AUTO_INCREMENT PRIMARY KEY,
    IdEntrega       INT          NOT NULL,
    IdConductor     INT          NULL,
    Motivo          VARCHAR(300) NOT NULL,
    Fecha           DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT FK_Incidencia_Entrega   FOREIGN KEY (IdEntrega)   REFERENCES Entrega(IdEntrega),
    CONSTRAINT FK_Incidencia_Conductor FOREIGN KEY (IdConductor) REFERENCES Conductor(IdConductor)
);

CREATE TABLE Reclamo (
    IdReclamo       INT AUTO_INCREMENT PRIMARY KEY,
    IdEntrega       INT          NOT NULL,
    Descripcion     VARCHAR(500) NOT NULL,
    Fecha           DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT FK_Reclamo_Entrega FOREIGN KEY (IdEntrega) REFERENCES Entrega(IdEntrega)
);

-- =====================================================================
--  DATOS INICIALES (catálogos y datos de prueba)
--  Usuarios de prueba: contraseña de todos = Vivero123*
--  (se guarda encriptada; el hash de abajo corresponde a esa contraseña)
-- =====================================================================

INSERT INTO Rol (Nombre, Descripcion) VALUES
('Administrador', 'Acceso total al sistema'),
('Supervisor',    'Supervisor / encargado de sucursal'),
('Jardinero',     'Trabajador que realiza tareas de cuidado'),
('Diseñador',     'Encargado de paquetes de decoración'),
('Conductor',     'Realiza las entregas'),
('Cliente',       'Cliente del vivero (pedidos en línea, catálogo)');

INSERT INTO Sucursal (Codigo, Nombre, Direccion, Telefono) VALUES
('SUC-001', 'Sucursal Central',     'Av. América #1234, Cochabamba',         '4401010'),
('SUC-002', 'Sucursal Quillacollo', 'Av. Blanco Galindo Km 12, Quillacollo', '4262020');

INSERT INTO Puesto (Nombre, Descripcion) VALUES
('Administrador',   'Administra el vivero'),
('Supervisor',      'Supervisa una sucursal'),
('Jardinero',       'Cuidado y mantenimiento de plantas'),
('Diseñador',       'Diseño de paquetes de decoración'),
('Conductor',       'Entrega de pedidos'),
('Vendedor',        'Atención y ventas en vivero');

INSERT INTO Especialidad (Nombre) VALUES
('Poda'), ('Riego'), ('Paisajismo'), ('Fumigación'), ('Trasplante'), ('Decoración de eventos');

INSERT INTO TipoActividad (Nombre) VALUES
('Riego'), ('Poda'), ('Fertilización'), ('Fumigación'), ('Trasplante'), ('Revisión general');

INSERT INTO Trabajador (CI, Nombres, Apellidos, Telefono, Correo, FechaIngreso, IdPuesto, IdSucursal, IdEspecialidad) VALUES
('1000001', 'Carlos',  'Mamani Rojas',   '70000001', 'carlos@vivero.bo',  '2024-01-15', 1, 1, NULL),
('1000002', 'Lucía',   'Fernández Vega', '70000002', 'lucia@vivero.bo',   '2024-02-01', 2, 1, 3),
('1000003', 'Pedro',   'Quispe Choque',  '70000003', 'pedro@vivero.bo',   '2024-03-10', 3, 1, 1),
('1000004', 'Rosa',    'Gutiérrez Paz',  '70000004', 'rosa@vivero.bo',    '2024-03-12', 3, 1, 2),
('1000005', 'Andrea',  'Salazar Ríos',   '70000005', 'andrea@vivero.bo',  '2024-05-20', 4, 1, 6),
('1000006', 'Miguel',  'Torrez López',   '70000006', 'miguel@vivero.bo',  '2024-06-01', 5, 1, NULL),
('1000007', 'Jorge',   'Vargas Cruz',    '70000007', 'jorge@vivero.bo',   '2024-07-01', 2, 2, 3),
('1000008', 'Elena',   'Rocha Mendoza',  '70000008', 'elena@vivero.bo',   '2024-08-05', 3, 2, 5),
('1000009', 'Diego',   'Arce Villca',    '70000009', 'diego@vivero.bo',   '2026-11-01', 3, NULL, 2);  -- sin sucursal (para probar UC4 de Mapache)

INSERT INTO Cliente (CiNit, Nombre, Telefono, Correo, Direccion) VALUES
('5000001', 'María López',          '71111111', 'maria@gmail.com',  'Calle Jordán #45'),
('5000002', 'Eventos Andinos SRL',  '71111112', 'contacto@andinos.bo', 'Av. Pando #300'),
('5000003', 'Juan Pérez',           '71111113', 'juan@gmail.com',   'Calle Sucre #12');

-- IdRol: 1 Administrador, 2 Supervisor, 3 Jardinero, 4 Diseñador, 5 Conductor, 6 Cliente
INSERT INTO Usuario (NombreUsuario, Correo, PasswordHash, IdRol, IdTrabajador, IdCliente) VALUES
('admin',      'carlos@vivero.bo', 'AQAAAAIAAYagAAAAEOaz4X19bygvrILnNByhyQ+W+odJsPUIsgeWc9ElVAaH8Jw/VrD8AW+h2RycSB9t0Q==', 1, 1, NULL),
('supervisor', 'lucia@vivero.bo',  'AQAAAAIAAYagAAAAEOaz4X19bygvrILnNByhyQ+W+odJsPUIsgeWc9ElVAaH8Jw/VrD8AW+h2RycSB9t0Q==', 2, 2, NULL),
('jardinero',  'pedro@vivero.bo',  'AQAAAAIAAYagAAAAEOaz4X19bygvrILnNByhyQ+W+odJsPUIsgeWc9ElVAaH8Jw/VrD8AW+h2RycSB9t0Q==', 3, 3, NULL),
('disenador',  'andrea@vivero.bo', 'AQAAAAIAAYagAAAAEOaz4X19bygvrILnNByhyQ+W+odJsPUIsgeWc9ElVAaH8Jw/VrD8AW+h2RycSB9t0Q==', 4, 5, NULL),
('conductor',  'miguel@vivero.bo', 'AQAAAAIAAYagAAAAEOaz4X19bygvrILnNByhyQ+W+odJsPUIsgeWc9ElVAaH8Jw/VrD8AW+h2RycSB9t0Q==', 5, 6, NULL),
('cliente',    'maria@gmail.com',  'AQAAAAIAAYagAAAAEOaz4X19bygvrILnNByhyQ+W+odJsPUIsgeWc9ElVAaH8Jw/VrD8AW+h2RycSB9t0Q==', 6, NULL, 1);

INSERT INTO Planta (Codigo, Nombre, Especie, Tipo, Descripcion, EsLote, Cantidad, EstadoPlanta, PrecioVenta, CostoReferencial, IdSucursal) VALUES
('PL-001', 'Rosa roja',        'Rosa gallica',          'Ornamental', 'Rosal de flor roja',          FALSE, 40, 'Lista para venta', 35.00, 15.00, 1),
('PL-002', 'Orquídea',         'Phalaenopsis',          'Ornamental', 'Orquídea blanca en maceta',   FALSE, 15, 'Lista para venta', 120.00, 60.00, 1),
('PL-003', 'Suculenta mix',    'Varias',                'Suculenta',  'Lote de suculentas pequeñas', TRUE,  100, 'Sana',            10.00, 4.00, 1),
('PL-004', 'Helecho',          'Nephrolepis exaltata',  'Ornamental', 'Helecho colgante',            FALSE, 25, 'En crecimiento',   45.00, 20.00, 1),
('PL-005', 'Limonero',         'Citrus limon',          'Frutal',     'Plantín de limonero',         FALSE, 12, 'Sana',             80.00, 35.00, 2),
('PL-006', 'Palmera areca',    'Dypsis lutescens',      'Ornamental', 'Palmera de interior',         FALSE, 8,  'Lista para venta', 150.00, 70.00, 1);

INSERT INTO Material (Nombre, Tipo, Unidad, CostoUnitario) VALUES
('Tierra preparada', 'Sustrato',    'bolsa',  25.00),
('Abono orgánico',   'Abono',       'kg',     12.00),
('Maceta mediana',   'Maceta',      'unidad', 18.00),
('Fertilizante NPK', 'Abono',       'kg',     30.00),
('Insecticida',      'Químico',     'litro',  55.00),
('Cinta decorativa', 'Decoración',  'metro',   2.50),
('Base de madera',   'Decoración',  'unidad', 40.00);

-- Stock por sucursal (IdMaterial, IdSucursal, Cantidad, UmbralMinimo)
INSERT INTO StockMaterial (IdMaterial, IdSucursal, Cantidad, UmbralMinimo) VALUES
(1, 1, 50, 10), (2, 1, 80, 20), (3, 1, 60, 15), (4, 1, 30, 10),
(5, 1, 4, 5),                    -- insecticida YA está bajo el umbral (para probar UC11)
(6, 1, 200, NULL),               -- sin umbral configurado (para probar UC11 1a / UC12)
(7, 1, 20, 5),
(1, 2, 20, 10), (2, 2, 25, 10), (3, 2, 15, 5);

INSERT INTO Proveedor (Nombre, TipoMaterial, Contacto, IdUsuarioRegistro) VALUES
('Agroinsumos Cochabamba', 'Abonos y fertilizantes', 'Ramiro Soliz - 72222221', 1),
('Macetas del Valle',      'Macetas',                'ventas@macetasvalle.bo', 1),
('Decor Eventos',          'Material decorativo',    'Paola Rivas - 72222223', 1);

INSERT INTO Servicio (Nombre, Descripcion, CostoReferencial) VALUES
('Diseño del ambiente',      'Diseño y propuesta visual de la decoración', 200.00),
('Instalación y montaje',    'Armado de la decoración en el lugar',         300.00),
('Transporte',               'Traslado de plantas y materiales',            100.00),
('Desmontaje',               'Retiro de la decoración tras el evento',      150.00);

INSERT INTO Vehiculo (Placa, Tipo) VALUES
('2345-ABC', 'Camioneta'),
('3456-BCD', 'Furgoneta');

INSERT INTO Conductor (Nombre, Telefono, Licencia, IdTrabajador) VALUES
('Miguel Torrez López', '70000006', 'LIC-55501', 6);

INSERT INTO DireccionEntrega (IdCliente, Direccion, Referencia) VALUES
(1, 'Calle Jordán #45', 'Puerta verde'),
(3, 'Calle Sucre #12',  'Frente a la plaza');

INSERT INTO Proyecto (IdCliente, Nombre, Descripcion, Ubicacion) VALUES
(2, 'Boda Pérez - Rojas', 'Decoración floral para boda de 150 invitados', 'Salón Los Pinos');
