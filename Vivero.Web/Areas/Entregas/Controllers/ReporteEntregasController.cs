using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Entregas.Controllers
{
    // =====================================================================
    //  CU08 - Ver reporte de entregas
    //  Equipo: Dinamita   |   Responsable: Jhery Rodriguez Aguilar
    //  Tablas que usa: Entrega, Conductor, Sucursal
    //  Solo el responsable modifica este archivo y la carpeta Views/ReporteEntregas/
    // =====================================================================
    [Area("Entregas")]
    [Authorize(Roles = RolesSistema.Administrador)]
    public class ReporteEntregasController : Controller
    {
        private readonly ViveroDbContext _context;

        public ReporteEntregasController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Entregas/ReporteEntregas
        public IActionResult Index()
        {
            // TODO (Jhery Rodriguez Aguilar): implementar el escenario principal y las extensiones de CU08
            return View();
        }
    }
}
