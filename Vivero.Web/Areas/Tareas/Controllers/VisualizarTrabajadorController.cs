using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Tareas.Controllers
{
    // =====================================================================
    //  UC4.5 - Visualizar trabajador
    //  Equipo: FRIENDS   |   Responsable: Mayerli Quinteros Silva
    //  Tablas que usa: Trabajador, Especialidad, Tarea
    //  Solo el responsable modifica este archivo y la carpeta Views/VisualizarTrabajador/
    // =====================================================================
    [Area("Tareas")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor)]
    public class VisualizarTrabajadorController : Controller
    {
        private readonly ViveroDbContext _context;

        public VisualizarTrabajadorController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Tareas/VisualizarTrabajador
        public IActionResult Index()
        {
            // TODO (Mayerli Quinteros Silva): implementar el escenario principal y las extensiones de UC4.5
            return View();
        }
    }
}
