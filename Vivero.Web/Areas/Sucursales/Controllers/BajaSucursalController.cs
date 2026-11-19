using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Sucursales.Controllers
{
    // =====================================================================
    //  UC3 - Dar de baja / desactivar sucursal
    //  Equipo: Mapache   |   Responsable: Alaitz Keyra Felipez Ledezma
    //  Tablas que usa: Sucursal, Trabajador, StockMaterial, MovimientoStock, Bitacora
    //  Solo el responsable modifica este archivo y la carpeta Views/BajaSucursal/
    // =====================================================================
    [Area("Sucursales")]
    [Authorize(Roles = RolesSistema.Administrador)]
    public class BajaSucursalController : Controller
    {
        private readonly ViveroDbContext _context;

        public BajaSucursalController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Sucursales/BajaSucursal
        public IActionResult Index()
        {
            // TODO (Alaitz Keyra Felipez Ledezma): implementar el escenario principal y las extensiones de UC3
            return View();
        }
    }
}
