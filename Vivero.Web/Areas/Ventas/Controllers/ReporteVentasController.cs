using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Ventas.Controllers
{
    // =====================================================================
    //  CU11 - Ver reporte de ventas
    //  Equipo: Dinamita   |   Responsable: Stif Lazo Barreto
    //  Tablas que usa: Venta, DetalleVenta, Sucursal
    //  Solo el responsable modifica este archivo y la carpeta Views/ReporteVentas/
    // =====================================================================
    [Area("Ventas")]
    [Authorize(Roles = RolesSistema.Administrador)]
    public class ReporteVentasController : Controller
    {
        private readonly ViveroDbContext _context;

        public ReporteVentasController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Ventas/ReporteVentas
        public IActionResult Index()
        {
            // TODO (Stif Lazo Barreto): implementar el escenario principal y las extensiones de CU11
            return View();
        }
    }
}
