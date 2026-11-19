using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Paquetes.Controllers
{
    // =====================================================================
    //  UC6.5 - Eliminar paquete
    //  Equipo: FRIENDS   |   Responsable: Maya Juana Mercado Ontiveros
    //  Tablas que usa: Paquete, AsignacionPaquete, Bitacora
    //  Solo el responsable modifica este archivo y la carpeta Views/EliminarPaquete/
    // =====================================================================
    [Area("Paquetes")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Disenador)]
    public class EliminarPaqueteController : Controller
    {
        private readonly ViveroDbContext _context;

        public EliminarPaqueteController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Paquetes/EliminarPaquete
        public IActionResult Index()
        {
            // TODO (Maya Juana Mercado Ontiveros): implementar el escenario principal y las extensiones de UC6.5
            return View();
        }
    }
}
