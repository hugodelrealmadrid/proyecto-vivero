using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Paquetes.Controllers
{
    // =====================================================================
    //  UC6.3 - Consultar catálogo de paquetes
    //  Equipo: FRIENDS   |   Responsable: Hugo David García Salazar
    //  Tablas que usa: Paquete, PaqueteDetalle, PaqueteImagen
    //  Solo el responsable modifica este archivo y la carpeta Views/CatalogoPaquetes/
    // =====================================================================
    [Area("Paquetes")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Disenador + "," + RolesSistema.Supervisor + "," + RolesSistema.Cliente)]
    public class CatalogoPaquetesController : Controller
    {
        private readonly ViveroDbContext _context;

        public CatalogoPaquetesController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Paquetes/CatalogoPaquetes
        public IActionResult Index()
        {
            // TODO (Hugo David García Salazar): implementar el escenario principal y las extensiones de UC6.3
            return View();
        }
    }
}
