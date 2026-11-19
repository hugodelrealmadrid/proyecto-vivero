using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Paquetes.Controllers
{
    // =====================================================================
    //  UC6.1 - Crear paquete (definir precio)
    //  Equipo: FRIENDS   |   Responsable: Maya Juana Mercado Ontiveros
    //  Tablas que usa: Paquete, PaqueteDetalle, PaqueteImagen, Planta, Material, Servicio
    //  Solo el responsable modifica este archivo y la carpeta Views/CrearPaquete/
    // =====================================================================
    [Area("Paquetes")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Disenador)]
    public class CrearPaqueteController : Controller
    {
        private readonly ViveroDbContext _context;

        public CrearPaqueteController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Paquetes/CrearPaquete
        public IActionResult Index()
        {
            // TODO (Maya Juana Mercado Ontiveros): implementar el escenario principal y las extensiones de UC6.1
            return View();
        }
    }
}
