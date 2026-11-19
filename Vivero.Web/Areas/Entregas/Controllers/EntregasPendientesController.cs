using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Entregas.Controllers
{
    // =====================================================================
    //  CU04 - Ver entregas pendientes
    //  Equipo: Dinamita   |   Responsable: Franz Villarroel Jattaco
    //  Tablas que usa: Entrega
    //  Solo el responsable modifica este archivo y la carpeta Views/EntregasPendientes/
    // =====================================================================
    [Area("Entregas")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor + "," + RolesSistema.Conductor)]
    public class EntregasPendientesController : Controller
    {
        private readonly ViveroDbContext _context;

        public EntregasPendientesController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Entregas/EntregasPendientes
        public IActionResult Index()
        {
            // TODO (Franz Villarroel Jattaco): implementar el escenario principal y las extensiones de CU04
            return View();
        }
    }
}
