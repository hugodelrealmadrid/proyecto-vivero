using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Plantas.Controllers
{
    // =====================================================================
    //  CU05 - Actualizar cantidad y estado de las plantas
    //  Equipo: Code³   |   Responsable: Emily Betel Condori Pocori
    //  Tablas que usa: Planta, HistorialPlanta
    //  Solo el responsable modifica este archivo y la carpeta Views/ActualizarPlanta/
    // =====================================================================
    [Area("Plantas")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor)]
    public class ActualizarPlantaController : Controller
    {
        private readonly ViveroDbContext _context;

        public ActualizarPlantaController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Plantas/ActualizarPlanta
        public IActionResult Index()
        {
            // TODO (Emily Betel Condori Pocori): implementar el escenario principal y las extensiones de CU05
            return View();
        }
    }
}
