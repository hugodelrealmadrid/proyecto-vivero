using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Entregas.Controllers
{
    // =====================================================================
    //  CU05 - Actualizar estado de entrega
    //  Equipo: Dinamita   |   Responsable: Jhery Rodriguez Aguilar
    //  Tablas que usa: Entrega, Conductor, Notificacion
    //  Solo el responsable modifica este archivo y la carpeta Views/ActualizarEstadoEntrega/
    // =====================================================================
    [Area("Entregas")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Conductor)]
    public class ActualizarEstadoEntregaController : Controller
    {
        private readonly ViveroDbContext _context;

        public ActualizarEstadoEntregaController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Entregas/ActualizarEstadoEntrega
        public IActionResult Index()
        {
            // TODO (Jhery Rodriguez Aguilar): implementar el escenario principal y las extensiones de CU05
            return View();
        }
    }
}
