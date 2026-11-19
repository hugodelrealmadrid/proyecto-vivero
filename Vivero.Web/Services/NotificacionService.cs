using Vivero.Web.Data;
using Vivero.Web.Models;

namespace Vivero.Web.Services
{
    // =====================================================================
    //  SERVICIO COMPARTIDO de notificaciones y bitácora.
    //  Lo usan Tareas (notificar al trabajador), Paquetes (enviar cotización
    //  al cliente) y Entregas (avisar cambios de estado).
    //
    //  Cómo usarlo en tu controlador:
    //      private readonly NotificacionService _notificaciones;
    //      public MiController(ViveroDbContext context, NotificacionService notificaciones) { ... }
    //      _notificaciones.NotificarUsuario(idUsuario, "Nueva tarea", "Se te asignó la tarea #5");
    //      await _context.SaveChangesAsync();   // se guarda junto con tu operación (atómico)
    //
    //  OJO: estos métodos NO llaman a SaveChanges, así la notificación se guarda
    //  en la MISMA transacción que tu operación (todo o nada).
    //  ARCHIVO COMPARTIDO: no lo modifiques en tu rama.
    // =====================================================================
    public class NotificacionService
    {
        private readonly ViveroDbContext _context;

        public NotificacionService(ViveroDbContext context)
        {
            _context = context;
        }

        public void NotificarUsuario(int idUsuarioDestino, string titulo, string mensaje)
        {
            Notificacion notificacion = new Notificacion();
            notificacion.IdUsuarioDestino = idUsuarioDestino;
            notificacion.Titulo = titulo;
            notificacion.Mensaje = mensaje;
            notificacion.Leida = false;
            notificacion.Fecha = DateTime.Now;
            _context.Notificaciones.Add(notificacion);
        }

        // Notifica al usuario ligado a un trabajador (si tiene cuenta)
        public void NotificarTrabajador(int idTrabajador, string titulo, string mensaje)
        {
            Usuario? usuario = _context.Usuarios.FirstOrDefault(u => u.IdTrabajador == idTrabajador && u.Activo);
            if (usuario != null)
            {
                NotificarUsuario(usuario.IdUsuario, titulo, mensaje);
            }
        }

        // Notifica al usuario ligado a un cliente (si tiene cuenta)
        public void NotificarCliente(int idCliente, string titulo, string mensaje)
        {
            Usuario? usuario = _context.Usuarios.FirstOrDefault(u => u.IdCliente == idCliente && u.Activo);
            if (usuario != null)
            {
                NotificarUsuario(usuario.IdUsuario, titulo, mensaje);
            }
        }

        public void RegistrarBitacora(string entidad, int idRegistro, string accion, string? detalle, int? idUsuario)
        {
            Bitacora registro = new Bitacora();
            registro.Entidad = entidad;
            registro.IdRegistro = idRegistro;
            registro.Accion = accion;
            registro.Detalle = detalle;
            registro.IdUsuario = idUsuario;
            registro.Fecha = DateTime.Now;
            _context.Bitacoras.Add(registro);
        }
    }
}
