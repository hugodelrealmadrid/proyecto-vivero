using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Tareas.Controllers
{
    // =====================================================================
    //  UC4.3 - Modificar / reasignar tarea
    //  Equipo: FRIENDS   |   Responsable: José Manuel Yanarico Quispe
    //  Tablas que usa: Tarea, HistorialTarea, ReservaMaterial, StockMaterial, Notificacion
    //  Solo el responsable modifica este archivo y la carpeta Views/ModificarTarea/
    // =====================================================================
    [Area("Tareas")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor)]
    public class ModificarTareaController : Controller
    {
        private readonly ViveroDbContext _context;

        public ModificarTareaController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Tareas/ModificarTarea
        public IActionResult Index()
        {
            // TODO (José Manuel Yanarico Quispe): implementar el escenario principal y las extensiones de UC4.3
            return View();
        }
    }
}
