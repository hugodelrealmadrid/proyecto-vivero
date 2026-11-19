using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Tareas.Controllers
{
    // =====================================================================
    //  UC4.1 - Registrar tarea
    //  Equipo: FRIENDS   |   Responsable: Mayerli Quinteros Silva
    //  Tablas que usa: Tarea, TareaMaterial, ReservaMaterial, StockMaterial, HistorialTarea, Notificacion
    //  Solo el responsable modifica este archivo y la carpeta Views/RegistrarTarea/
    // =====================================================================
    [Area("Tareas")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor)]
    public class RegistrarTareaController : Controller
    {
        private readonly ViveroDbContext _context;

        public RegistrarTareaController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Tareas/RegistrarTarea
        public IActionResult Index()
        {
            // TODO (Mayerli Quinteros Silva): implementar el escenario principal y las extensiones de UC4.1
            return View();
        }
    }
}
