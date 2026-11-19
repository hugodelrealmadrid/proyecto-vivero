using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vivero.Web.Helpers;

namespace Vivero.Web.Areas.Sucursales.Controllers
{
    // Página principal del módulo "Gestión de Sucursales" (Equipo Mapache).
    [Area("Sucursales")]
    [Authorize(Roles = RolesSistema.Administrador)]
    public class InicioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
