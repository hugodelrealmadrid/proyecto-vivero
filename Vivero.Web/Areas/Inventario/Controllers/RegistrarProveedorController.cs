using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Inventario.Controllers
{
    // =====================================================================
    //  UC6 - Registrar proveedor
    //  Equipo: Mapache   |   Responsable: Yubidsa Gabriela Bueno Cocarico
    //  Tablas que usa: Proveedor
    //  Solo el responsable modifica este archivo y la carpeta Views/RegistrarProveedor/
    // =====================================================================
    [Area("Inventario")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor)]
    public class RegistrarProveedorController : Controller
    {
        private readonly ViveroDbContext _context;

        public RegistrarProveedorController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Inventario/RegistrarProveedor
        public IActionResult Index()
        {
            // TODO (Yubidsa Gabriela Bueno Cocarico): implementar el escenario principal y las extensiones de UC6
            return View();
        }
    }
}
