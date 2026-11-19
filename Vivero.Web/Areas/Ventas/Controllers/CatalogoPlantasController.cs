using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Ventas.Controllers
{
    // =====================================================================
    //  CU16 - Consultar catálogo de plantas
    //  Equipo: Dinamita   |   Responsable: Jhamil Flores Choque
    //  Tablas que usa: Planta
    //  Solo el responsable modifica este archivo y la carpeta Views/CatalogoPlantas/
    // =====================================================================
    [Area("Ventas")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor + "," + RolesSistema.Cliente)]
    public class CatalogoPlantasController : Controller
    {
        private readonly ViveroDbContext _context;

        public CatalogoPlantasController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Ventas/CatalogoPlantas
        public IActionResult Index()
        {
            // TODO (Jhamil Flores Choque): implementar el escenario principal y las extensiones de CU16
            return View();
        }
    }
}
