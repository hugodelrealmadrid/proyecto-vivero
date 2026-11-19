using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Ventas.Controllers
{
    // =====================================================================
    //  CU10 - Ver historial de ventas
    //  Equipo: Dinamita   |   Responsable: Stif Lazo Barreto
    //  Tablas que usa: Venta, DetalleVenta
    //  Solo el responsable modifica este archivo y la carpeta Views/HistorialVentas/
    // =====================================================================
    [Area("Ventas")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor)]
    public class HistorialVentasController : Controller
    {
        private readonly ViveroDbContext _context;

        public HistorialVentasController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Ventas/HistorialVentas
        public IActionResult Index()
        {
            // TODO (Stif Lazo Barreto): implementar el escenario principal y las extensiones de CU10
            return View();
        }
    }
}
