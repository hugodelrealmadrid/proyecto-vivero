using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Inventario.Controllers
{
    // =====================================================================
    //  UC11 - Notificar bajo stock de material
    //  Equipo: Mapache   |   Responsable: Roberto Carlos Herrera Coca
    //  Tablas que usa: AlertaStock, StockMaterial, Notificacion
    //  Solo el responsable modifica este archivo y la carpeta Views/AlertasStock/
    // =====================================================================
    [Area("Inventario")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor)]
    public class AlertasStockController : Controller
    {
        private readonly ViveroDbContext _context;

        public AlertasStockController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Inventario/AlertasStock
        public IActionResult Index()
        {
            // TODO (Roberto Carlos Herrera Coca): implementar el escenario principal y las extensiones de UC11
            return View();
        }
    }
}
