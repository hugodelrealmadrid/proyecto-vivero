using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Personal.Controllers
{
    // =====================================================================
    //  CU01 - Registrar personal y asignar puesto
    //  Equipo: Code³   |   Responsable: Brener Garcia Flores
    //  Tablas que usa: Trabajador, Puesto, Sucursal, Especialidad
    //  Solo el responsable modifica este archivo y la carpeta Views/RegistrarPersonal/
    // =====================================================================
    [Area("Personal")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor)]
    public class RegistrarPersonalController : Controller
    {
        private readonly ViveroDbContext _context;

        public RegistrarPersonalController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Personal/RegistrarPersonal
        public IActionResult Index()
        {
            // TODO (Brener Garcia Flores): implementar el escenario principal y las extensiones de CU01
            return View();
        }
    }
}
