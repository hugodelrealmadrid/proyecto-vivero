using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Personal.Controllers
{
    // =====================================================================
    //  CU03 - Dar de baja al personal
    //  Equipo: Code³   |   Responsable: Sidny Abigail Mendoza Vargas
    //  Tablas que usa: Trabajador, Bitacora
    //  Solo el responsable modifica este archivo y la carpeta Views/BajaPersonal/
    // =====================================================================
    [Area("Personal")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor)]
    public class BajaPersonalController : Controller
    {
        private readonly ViveroDbContext _context;

        public BajaPersonalController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Personal/BajaPersonal
        public IActionResult Index()
        {
            // TODO (Sidny Abigail Mendoza Vargas): implementar el escenario principal y las extensiones de CU03
            return View();
        }
    }
}
