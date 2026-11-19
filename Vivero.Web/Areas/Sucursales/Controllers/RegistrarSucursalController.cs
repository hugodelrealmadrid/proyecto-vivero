using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Sucursales.Controllers
{
    // =====================================================================
    //  UC1 - Registrar sucursal
    //  Equipo: Mapache   |   Responsable: Alaitz Keyra Felipez Ledezma
    //  Tablas que usa: Sucursal, Bitacora
    //  Solo el responsable modifica este archivo y la carpeta Views/RegistrarSucursal/
    // =====================================================================
    [Area("Sucursales")]
    [Authorize(Roles = RolesSistema.Administrador)]
    public class RegistrarSucursalController : Controller
    {
        private readonly ViveroDbContext _context;

        public RegistrarSucursalController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Sucursales/RegistrarSucursal
        public IActionResult Index()
        {
            // TODO (Alaitz Keyra Felipez Ledezma): implementar el escenario principal y las extensiones de UC1
            return View();
        }
    }
}
