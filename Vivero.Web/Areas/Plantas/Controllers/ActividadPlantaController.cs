using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Plantas.Controllers
{
    // =====================================================================
    //  CU06 - Registrar actividades / reportes de cuidado
    //  Equipo: Code³   |   Responsable: Ariel Rodrigo Rocabado
    //  Tablas que usa: ActividadPlanta, TipoActividad, Planta, Trabajador
    //  Solo el responsable modifica este archivo y la carpeta Views/ActividadPlanta/
    // =====================================================================
    [Area("Plantas")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor + "," + RolesSistema.Jardinero)]
    public class ActividadPlantaController : Controller
    {
        private readonly ViveroDbContext _context;

        public ActividadPlantaController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Plantas/ActividadPlanta
        public IActionResult Index()
        {
            // TODO (Ariel Rodrigo Rocabado): implementar el escenario principal y las extensiones de CU06
            return View();
        }
    }
}
