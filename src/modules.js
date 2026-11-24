/* Catálogo de navegación. Los nombres técnicos conservan las entidades del diagrama.
 * Cada compañero puede añadir su componente en src/modules/registry.jsx.
 * No se almacenan ni se simulan registros. Las columnas son etiquetas de interfaz,
 * no una migración SQL: confirmar tipos y campos opcionales con el esquema original.
 */
export const modules = [
  {
    key: "plantas",
    name: "Plantas e inventario",
    icon: "plant",
    description: "El cuidado comienza desde la raíz.",
    entities: [
      [
        "planta",
        "Plantas",
        "ID,Nombre,Especie,Descripción,Precio de venta,Activa",
      ],
      [
        "lote_planta",
        "Lotes de plantas",
        "ID,Planta,Sucursal,Cantidad,Estado,Fecha de ingreso,Fecha de baja,Motivo de baja,Registrado por",
      ],
      [
        "actividad_cuidado",
        "Actividades de cuidado",
        "ID,Lote,Tipo,Fecha,Responsable,Observaciones,Próxima fecha",
      ],
    ],
  },
  {
    key: "ventas",
    name: "Clientes y ventas",
    icon: "cart",
    description: "Cada pedido, una nueva historia.",
    entities: [
      ["cliente", "Clientes", "ID,Nombre,Teléfono,Dirección,Correo,Creado en"],
      [
        "pedido",
        "Pedidos",
        "ID,Cliente,Sucursal,Supervisor,Fecha,Estado,Total,Motivo,Resuelto en",
      ],
      [
        "detalle_pedido",
        "Detalle de pedidos",
        "ID,Pedido,Planta,Cantidad,Precio unitario,Subtotal",
      ],
      ["venta", "Ventas", "ID,Sucursal,Supervisor,Cliente,Fecha,Total"],
      [
        "detalle_venta",
        "Detalle de ventas",
        "ID,Venta,Lote,Cantidad,Precio unitario,Subtotal",
      ],
      [
        "despacho_pedido",
        "Despachos de pedidos",
        "ID,Detalle del pedido,Lote,Cantidad",
      ],
    ],
  },
  {
    key: "entregas",
    name: "Entregas",
    icon: "truck",
    description: "La naturaleza llega a su destino.",
    entities: [
      [
        "entrega",
        "Entregas",
        "ID,Pedido,Dirección de entrega,Vehículo,Registrada por,Estado,Fecha y hora,Código de confirmación,Foto",
      ],
      [
        "solicitud_conductor",
        "Solicitudes de conductor",
        "ID,Entrega,Conductor,Estado,Fecha y hora",
      ],
      ["vehiculo", "Vehículos", "ID,Placa,Tipo,Activo"],
      [
        "direccion_entrega",
        "Direcciones de entrega",
        "ID,Cliente,Dirección,Referencia",
      ],
      [
        "incidencia",
        "Incidencias",
        "ID,Entrega,Motivo,Fecha y hora,Registrada por",
      ],
      ["reclamo", "Reclamos", "ID,Entrega,Descripción,Fecha"],
    ],
  },
  {
    key: "compras",
    name: "Compras y sucursales",
    icon: "box",
    description: "Todo lo que tu jardín necesita.",
    entities: [
      [
        "sucursal",
        "Sucursales",
        "ID,Código,Nombre,Dirección,Teléfono,Estado,Creada por,Creada en,Baja por,Baja en",
      ],
      [
        "proveedor",
        "Proveedores",
        "ID,Nombre,Tipo de material,Contacto,Estado,Registrado por,Creado en,Baja por,Baja en",
      ],
      [
        "compra",
        "Compras",
        "ID,Comprobante,Proveedor,Sucursal,Usuario,Fecha,Total,Estado",
      ],
      [
        "detalle_compra",
        "Detalle de compras",
        "ID,Compra,Material,Cantidad,Precio unitario,Subtotal",
      ],
      ["material", "Materiales", "ID,Nombre,Tipo,Unidad de medida,Activo"],
      [
        "stock_material",
        "Stock de materiales",
        "Material,Sucursal,Cantidad,Umbral mínimo",
      ],
      [
        "movimiento_material",
        "Movimientos de material",
        "ID,Material,Sucursal,Tipo,Cantidad,Compra,Usuario,Fecha",
      ],
      [
        "alerta_stock",
        "Alertas de stock",
        "ID,Material,Sucursal,Fecha,Mensaje,Cantidad actual,Umbral,Atendida,Atendida por,Atendida en,Compra",
      ],
    ],
  },
  {
    key: "tareas",
    name: "Tareas y decoración",
    icon: "tools",
    description: "Ideas que se convierten en jardines.",
    entities: [
      [
        "tarea",
        "Tareas",
        "ID,Sucursal,Tipo de trabajo,Descripción,Ubicación,Fecha programada,Prioridad,Estado,Supervisor,Trabajador,Proyecto,Creada en",
      ],
      [
        "historial_tarea",
        "Historial de tareas",
        "ID,Tarea,Estado anterior,Estado nuevo,Fecha y hora,Usuario,Motivo",
      ],
      [
        "proyecto",
        "Proyectos",
        "ID,Cliente,Nombre,Ubicación,Estado,Fecha de inicio,Fecha de fin",
      ],
      [
        "paquete",
        "Paquetes de decoración",
        "ID,Nombre,Tipo de evento,Estilo,Descripción,Imagen,Costo referencial,Precio,Estado,Personalizable,Creado por,Creado en",
      ],
      [
        "paquete_elemento",
        "Elementos de paquetes",
        "ID,Paquete,Tipo,Planta,Material,Servicio,Cantidad",
      ],
      ["servicio", "Servicios", "ID,Nombre,Descripción,Costo referencial"],
      [
        "asignacion_paquete",
        "Asignaciones de paquetes",
        "ID,Paquete,Cliente,Proyecto,Fecha de servicio,Ubicación,Condiciones,Precio cotizado,Fecha de cotización,Aprobado en,Asignado por",
      ],
      [
        "reserva_material",
        "Reservas de materiales",
        "ID,Material,Sucursal,Cantidad,Estado,Tarea,Asignación de paquete,Creada en",
      ],
    ],
  },
  {
    key: "personal",
    name: "Personal",
    icon: "people",
    description: "Las manos detrás de cada brote.",
    entities: [
      [
        "usuario",
        "Usuarios",
        "ID,Nombre,Usuario,Clave hash,Tipo,Estado,Creado en",
      ],
      [
        "empleado",
        "Empleados",
        "Usuario,Sucursal,Documento,Teléfono,Disponible,Fecha de ingreso,Fecha de baja,Motivo de baja",
      ],
      ["jardinero", "Jardineros", "Usuario,Especialidad"],
      ["disenador", "Diseñadores", "Usuario,Especialidad"],
      ["conductor", "Conductores", "Usuario,Licencia de conducir"],
    ],
  },
].map((group) => ({
  ...group,
  entities: group.entities.map(([key, name, fields]) => ({
    key,
    name,
    fields: fields.split(","),
  })),
}));
export const notifications = {
  key: "notificacion",
  name: "Notificaciones",
  fields: ["ID", "Usuario", "Cliente", "Tipo", "Mensaje", "Fecha", "Leída"],
};
