using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Paquetes.Controllers
{
    // =====================================================================
    //  UC6.4 - Modificar paquete
    //  Equipo: FRIENDS   |   Responsable: Mayerli Quinteros Silva
    //  Tablas que usa: Paquete, PaqueteDetalle, ReservaMaterial, StockMaterial, Cotizacion, Bitacora, Notificacion
    //  Solo el responsable modifica este archivo y la carpeta Views/ModificarPaquete/
    // =====================================================================
    [Area("Paquetes")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Disenador)]
    public class ModificarPaqueteController : Controller
    {
        private readonly ViveroDbContext _context;

        public ModificarPaqueteController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Paquetes/ModificarPaquete
        public IActionResult Index()
        {
            // TODO (Mayerli Quinteros Silva): implementar el escenario principal y las extensiones de UC6.4
            return View();
        }
    }
}
