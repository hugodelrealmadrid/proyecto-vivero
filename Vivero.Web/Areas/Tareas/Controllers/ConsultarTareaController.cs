using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Tareas.Controllers
{
    // =====================================================================
    //  UC4.2 - Consultar estado de tarea
    //  Equipo: FRIENDS   |   Responsable: Maya Juana Mercado Ontiveros
    //  Tablas que usa: Tarea, HistorialTarea, TareaMaterial
    //  Solo el responsable modifica este archivo y la carpeta Views/ConsultarTarea/
    // =====================================================================
    [Area("Tareas")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor + "," + RolesSistema.Jardinero)]
    public class ConsultarTareaController : Controller
    {
        private readonly ViveroDbContext _context;

        public ConsultarTareaController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Tareas/ConsultarTarea
        public IActionResult Index()
        {
            // TODO (Maya Juana Mercado Ontiveros): implementar el escenario principal y las extensiones de UC4.2
            return View();
        }
    }
}
