using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Inventario.Controllers
{
    // =====================================================================
    //  UC9 - Consultar stock de materiales
    //  Equipo: Mapache   |   Responsable: Roberto Carlos Herrera Coca
    //  Tablas que usa: StockMaterial, Material, Sucursal
    //  Solo el responsable modifica este archivo y la carpeta Views/StockMateriales/
    // =====================================================================
    [Area("Inventario")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor)]
    public class StockMaterialesController : Controller
    {
        private readonly ViveroDbContext _context;

        public StockMaterialesController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Inventario/StockMateriales
        public IActionResult Index()
        {
            // TODO (Roberto Carlos Herrera Coca): implementar el escenario principal y las extensiones de UC9
            return View();
        }
    }
}
