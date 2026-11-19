using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Tareas.Controllers
{
    // =====================================================================
    //  UC4.4 - Eliminar / cancelar tarea
    //  Equipo: FRIENDS   |   Responsable: Hugo David García Salazar
    //  Tablas que usa: Tarea, HistorialTarea, ReservaMaterial, StockMaterial, Bitacora, Notificacion
    //  Solo el responsable modifica este archivo y la carpeta Views/CancelarTarea/
    // =====================================================================
    [Area("Tareas")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor)]
    public class CancelarTareaController : Controller
    {
        private readonly ViveroDbContext _context;

        public CancelarTareaController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Tareas/CancelarTarea
        public IActionResult Index()
        {
            // TODO (Hugo David García Salazar): implementar el escenario principal y las extensiones de UC4.4
            return View();
        }
    }
}
