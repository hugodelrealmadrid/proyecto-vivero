using System.Security.Claims;

namespace Vivero.Web.Helpers
{
    // =====================================================================
    //  Nombres de los roles (deben ser iguales a la tabla Rol de la BD).
    //  Úsalos así en tus controladores:
    //      [Authorize(Roles = RolesSistema.Supervisor)]
    //      [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor)]
    //  ARCHIVO COMPARTIDO: no lo modifiques en tu rama.
    // =====================================================================
    public static class RolesSistema
    {
        public const string Administrador = "Administrador";
        public const string Supervisor = "Supervisor";
        public const string Jardinero = "Jardinero";
        public const string Disenador = "Diseñador";
        public const string Conductor = "Conductor";
        public const string Cliente = "Cliente";
    }

    // =====================================================================
    //  Datos del usuario que inició sesión (se guardan en la cookie al hacer login).
    //  Ejemplo dentro de un controlador:
    //      int idUsuario = UsuarioActual.ObtenerIdUsuario(User);
    //      int? idTrabajador = UsuarioActual.ObtenerIdTrabajador(User);
    // =====================================================================
    public static class UsuarioActual
    {
        public const string ClaimIdUsuario = "IdUsuario";
        public const string ClaimIdTrabajador = "IdTrabajador";
        public const string ClaimIdCliente = "IdCliente";
        public const string ClaimIdSucursal = "IdSucursal";

        public static int ObtenerIdUsuario(ClaimsPrincipal usuario)
        {
            string? valor = usuario.FindFirstValue(ClaimIdUsuario);
            if (valor == null)
            {
                return 0;
            }
            return int.Parse(valor);
        }

        public static int? ObtenerIdTrabajador(ClaimsPrincipal usuario)
        {
            return LeerEnteroOpcional(usuario, ClaimIdTrabajador);
        }

        public static int? ObtenerIdCliente(ClaimsPrincipal usuario)
        {
            return LeerEnteroOpcional(usuario, ClaimIdCliente);
        }

        public static int? ObtenerIdSucursal(ClaimsPrincipal usuario)
        {
            return LeerEnteroOpcional(usuario, ClaimIdSucursal);
        }

        private static int? LeerEnteroOpcional(ClaimsPrincipal usuario, string nombreClaim)
        {
            string? valor = usuario.FindFirstValue(nombreClaim);
            if (string.IsNullOrEmpty(valor))
            {
                return null;
            }
            return int.Parse(valor);
        }
    }
}
