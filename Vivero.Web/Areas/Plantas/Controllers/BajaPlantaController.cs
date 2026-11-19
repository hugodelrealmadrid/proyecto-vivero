using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Plantas.Controllers
{
    // =====================================================================
    //  CU07 - Dar de baja una planta o lote
    //  Equipo: Code³   |   Responsable: Sidny Abigail Mendoza Vargas
    //  Tablas que usa: Planta, HistorialPlanta
    //  Solo el responsable modifica este archivo y la carpeta Views/BajaPlanta/
    // =====================================================================
    [Area("Plantas")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor)]
    public class BajaPlantaController : Controller
    {
        private readonly ViveroDbContext _context;

        public BajaPlantaController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Plantas/BajaPlanta
        public IActionResult Index()
        {
            // TODO (Sidny Abigail Mendoza Vargas): implementar el escenario principal y las extensiones de CU07
            return View();
        }
    }
}
