using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vivero.Web.Helpers;

namespace Vivero.Web.Areas.Personal.Controllers
{
    // Página principal del módulo "Gestión de Personal" (Equipo Code³).
    [Area("Personal")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor)]
    public class InicioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
