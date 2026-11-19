using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Inventario.Controllers
{
    // =====================================================================
    //  UC10 - Dar de baja proveedor
    //  Equipo: Mapache   |   Responsable: Roberto Carlos Herrera Coca
    //  Tablas que usa: Proveedor, Compra
    //  Solo el responsable modifica este archivo y la carpeta Views/BajaProveedor/
    // =====================================================================
    [Area("Inventario")]
    [Authorize(Roles = RolesSistema.Administrador)]
    public class BajaProveedorController : Controller
    {
        private readonly ViveroDbContext _context;

        public BajaProveedorController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Inventario/BajaProveedor
        public IActionResult Index()
        {
            // TODO (Roberto Carlos Herrera Coca): implementar el escenario principal y las extensiones de UC10
            return View();
        }
    }
}
