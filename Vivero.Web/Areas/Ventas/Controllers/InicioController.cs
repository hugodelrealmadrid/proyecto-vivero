using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vivero.Web.Helpers;

namespace Vivero.Web.Areas.Ventas.Controllers
{
    // Página principal del módulo "Ventas y Pedidos" (Equipo Dinamita).
    [Area("Ventas")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor + "," + RolesSistema.Cliente)]
    public class InicioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
