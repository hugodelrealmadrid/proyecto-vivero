using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vivero.Web.Helpers;

namespace Vivero.Web.Areas.Entregas.Controllers
{
    // Página principal del módulo "Gestión de Entregas" (Equipo Dinamita).
    [Area("Entregas")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor + "," + RolesSistema.Conductor + "," + RolesSistema.Cliente)]
    public class InicioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
