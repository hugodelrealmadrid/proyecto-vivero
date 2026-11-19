using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vivero.Web.Helpers;

namespace Vivero.Web.Areas.Paquetes.Controllers
{
    // Página principal del módulo "Paquetes de Decoración" (Equipo FRIENDS).
    [Area("Paquetes")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Disenador + "," + RolesSistema.Supervisor + "," + RolesSistema.Cliente)]
    public class InicioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
