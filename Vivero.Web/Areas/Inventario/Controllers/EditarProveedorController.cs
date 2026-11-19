using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Inventario.Controllers
{
    // =====================================================================
    //  UC7 - Editar datos de proveedor
    //  Equipo: Mapache   |   Responsable: Yubidsa Gabriela Bueno Cocarico
    //  Tablas que usa: Proveedor
    //  Solo el responsable modifica este archivo y la carpeta Views/EditarProveedor/
    // =====================================================================
    [Area("Inventario")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor)]
    public class EditarProveedorController : Controller
    {
        private readonly ViveroDbContext _context;

        public EditarProveedorController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Inventario/EditarProveedor
        public IActionResult Index()
        {
            // TODO (Yubidsa Gabriela Bueno Cocarico): implementar el escenario principal y las extensiones de UC7
            return View();
        }
    }
}
