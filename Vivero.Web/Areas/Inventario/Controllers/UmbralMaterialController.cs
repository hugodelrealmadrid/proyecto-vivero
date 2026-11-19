using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Inventario.Controllers
{
    // =====================================================================
    //  UC12 - Configurar umbral mínimo de material
    //  Equipo: Mapache   |   Responsable: Roberto Carlos Herrera Coca
    //  Tablas que usa: StockMaterial
    //  Solo el responsable modifica este archivo y la carpeta Views/UmbralMaterial/
    // =====================================================================
    [Area("Inventario")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor)]
    public class UmbralMaterialController : Controller
    {
        private readonly ViveroDbContext _context;

        public UmbralMaterialController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Inventario/UmbralMaterial
        public IActionResult Index()
        {
            // TODO (Roberto Carlos Herrera Coca): implementar el escenario principal y las extensiones de UC12
            return View();
        }
    }
}
