using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vivero.Web.Helpers;

namespace Vivero.Web.Areas.Tareas.Controllers
{
    // Página principal del módulo "Asignación de Tareas" (Equipo FRIENDS).
    [Area("Tareas")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor + "," + RolesSistema.Jardinero)]
    public class InicioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
