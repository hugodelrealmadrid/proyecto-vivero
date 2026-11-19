using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Personal.Controllers
{
    // =====================================================================
    //  CU02 - Actualizar datos del personal
    //  Equipo: Code³   |   Responsable: Emily Betel Condori Pocori
    //  Tablas que usa: Trabajador, Puesto
    //  Solo el responsable modifica este archivo y la carpeta Views/ActualizarPersonal/
    // =====================================================================
    [Area("Personal")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor)]
    public class ActualizarPersonalController : Controller
    {
        private readonly ViveroDbContext _context;

        public ActualizarPersonalController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Personal/ActualizarPersonal
        public IActionResult Index()
        {
            // TODO (Emily Betel Condori Pocori): implementar el escenario principal y las extensiones de CU02
            return View();
        }
    }
}
