using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Entregas.Controllers
{
    // =====================================================================
    //  CU06 - Registrar incidencia de entrega
    //  Equipo: Dinamita   |   Responsable: Jhery Rodriguez Aguilar
    //  Tablas que usa: Incidencia, Entrega, Conductor
    //  Solo el responsable modifica este archivo y la carpeta Views/RegistrarIncidencia/
    // =====================================================================
    [Area("Entregas")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Conductor)]
    public class RegistrarIncidenciaController : Controller
    {
        private readonly ViveroDbContext _context;

        public RegistrarIncidenciaController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Entregas/RegistrarIncidencia
        public IActionResult Index()
        {
            // TODO (Jhery Rodriguez Aguilar): implementar el escenario principal y las extensiones de CU06
            return View();
        }
    }
}
