using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Sucursales.Controllers
{
    // =====================================================================
    //  UC5 - Consultar listado de sucursales
    //  Equipo: Mapache   |   Responsable: Yubidsa Gabriela Bueno Cocarico
    //  Tablas que usa: Sucursal, Trabajador
    //  Solo el responsable modifica este archivo y la carpeta Views/ListadoSucursales/
    // =====================================================================
    [Area("Sucursales")]
    [Authorize(Roles = RolesSistema.Administrador)]
    public class ListadoSucursalesController : Controller
    {
        private readonly ViveroDbContext _context;

        public ListadoSucursalesController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Sucursales/ListadoSucursales
        public IActionResult Index()
        {
            // TODO (Yubidsa Gabriela Bueno Cocarico): implementar el escenario principal y las extensiones de UC5
            return View();
        }
    }
}
