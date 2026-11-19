using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Entregas.Controllers
{
    // =====================================================================
    //  CU03 - Asignar conductor
    //  Equipo: Dinamita   |   Responsable: Franz Villarroel Jattaco
    //  Tablas que usa: Entrega, Conductor, SolicitudConductor
    //  Solo el responsable modifica este archivo y la carpeta Views/AsignarConductor/
    // =====================================================================
    [Area("Entregas")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor)]
    public class AsignarConductorController : Controller
    {
        private readonly ViveroDbContext _context;

        public AsignarConductorController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Entregas/AsignarConductor
        public IActionResult Index()
        {
            // TODO (Franz Villarroel Jattaco): implementar el escenario principal y las extensiones de CU03
            return View();
        }
    }
}
