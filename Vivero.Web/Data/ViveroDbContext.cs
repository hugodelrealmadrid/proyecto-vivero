using Microsoft.EntityFrameworkCore;
using Vivero.Web.Models;

namespace Vivero.Web.Data
{
    // =====================================================================
    //  CONTEXTO DE BASE DE DATOS (Entity Framework Core + MySQL/MariaDB)
    //  ARCHIVO COMPARTIDO: NO lo modifiques en tu rama.
    //  Ya tiene TODAS las tablas de todos los equipos. Si necesitas una
    //  tabla o columna nueva, pídela a la encargada de la BD.
    //
    //  Se trabaja "Database First": la BD se crea con database/vivero_db.sql
    //  y NO se usan migraciones (Add-Migration), para evitar conflictos.
    //  "= null!" significa: EF Core llena esta propiedad automáticamente.
    // =====================================================================
    public class ViveroDbContext : DbContext
    {
        public ViveroDbContext(DbContextOptions<ViveroDbContext> options) : base(options)
        {
        }

        // ---- Comun (BASE COMÚN (solo la modifica la encargada de la BD)) ----
        public DbSet<Sucursal> Sucursales { get; set; } = null!;
        public DbSet<Rol> Roles { get; set; } = null!;
        public DbSet<Puesto> Puestos { get; set; } = null!;
        public DbSet<Especialidad> Especialidades { get; set; } = null!;
        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Material> Materiales { get; set; } = null!;
        public DbSet<Servicio> Servicios { get; set; } = null!;
        public DbSet<Notificacion> Notificaciones { get; set; } = null!;
        public DbSet<Bitacora> Bitacoras { get; set; } = null!;

        // ---- Inventario (Equipo Mapache) ----
        public DbSet<Proveedor> Proveedores { get; set; } = null!;
        public DbSet<StockMaterial> StocksMaterial { get; set; } = null!;
        public DbSet<MovimientoStock> MovimientosStock { get; set; } = null!;
        public DbSet<AlertaStock> AlertasStock { get; set; } = null!;
        public DbSet<Compra> Compras { get; set; } = null!;
        public DbSet<DetalleCompra> DetallesCompra { get; set; } = null!;

        // ---- Personal (Equipo Code³) ----
        public DbSet<Trabajador> Trabajadores { get; set; } = null!;

        // ---- Plantas (Equipo Code³) ----
        public DbSet<Planta> Plantas { get; set; } = null!;
        public DbSet<HistorialPlanta> HistorialPlantas { get; set; } = null!;
        public DbSet<TipoActividad> TiposActividad { get; set; } = null!;
        public DbSet<ActividadPlanta> ActividadesPlanta { get; set; } = null!;
        public DbSet<OrdenCompra> OrdenesCompra { get; set; } = null!;
        public DbSet<DetalleOrdenCompra> DetallesOrdenCompra { get; set; } = null!;

        // ---- Paquetes (Equipo FRIENDS) ----
        public DbSet<Proyecto> Proyectos { get; set; } = null!;
        public DbSet<Paquete> Paquetes { get; set; } = null!;
        public DbSet<PaqueteImagen> PaqueteImagenes { get; set; } = null!;
        public DbSet<PaqueteDetalle> PaqueteDetalles { get; set; } = null!;
        public DbSet<AsignacionPaquete> AsignacionesPaquete { get; set; } = null!;
        public DbSet<Cotizacion> Cotizaciones { get; set; } = null!;

        // ---- Tareas (Equipo FRIENDS) ----
        public DbSet<Tarea> Tareas { get; set; } = null!;
        public DbSet<TareaMaterial> TareaMateriales { get; set; } = null!;
        public DbSet<HistorialTarea> HistorialTareas { get; set; } = null!;
        public DbSet<ReservaMaterial> ReservasMaterial { get; set; } = null!;

        // ---- Ventas (Equipo Dinamita) ----
        public DbSet<Venta> Ventas { get; set; } = null!;
        public DbSet<DetalleVenta> DetallesVenta { get; set; } = null!;
        public DbSet<Pedido> Pedidos { get; set; } = null!;
        public DbSet<DetallePedido> DetallesPedido { get; set; } = null!;

        // ---- Entregas (Equipo Dinamita) ----
        public DbSet<DireccionEntrega> DireccionesEntrega { get; set; } = null!;
        public DbSet<Vehiculo> Vehiculos { get; set; } = null!;
        public DbSet<Conductor> Conductores { get; set; } = null!;
        public DbSet<Entrega> Entregas { get; set; } = null!;
        public DbSet<SolicitudConductor> SolicitudesConductor { get; set; } = null!;
        public DbSet<Incidencia> Incidencias { get; set; } = null!;
        public DbSet<Reclamo> Reclamos { get; set; } = null!;
    }
}
