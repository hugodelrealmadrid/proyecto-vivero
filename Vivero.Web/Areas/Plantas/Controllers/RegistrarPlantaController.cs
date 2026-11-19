using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Plantas.Controllers
{
    // =====================================================================
    //  CU04 - Registrar planta
    //  Equipo: Code³   |   Responsable: Brener Garcia Flores
    //  Tablas que usa: Planta, Sucursal
    //  Solo el responsable modifica este archivo y la carpeta Views/RegistrarPlanta/
    // =====================================================================
    [Area("Plantas")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor + "," + RolesSistema.Jardinero)]
    public class RegistrarPlantaController : Controller
    {
        private readonly ViveroDbContext _context;

        public RegistrarPlantaController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Plantas/RegistrarPlanta
        public IActionResult Index()
        {
            // TODO (Brener Garcia Flores): implementar el escenario principal y las extensiones de CU04
            return View();
        }
    }
}
