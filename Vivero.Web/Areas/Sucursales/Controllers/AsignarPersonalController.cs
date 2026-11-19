using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Sucursales.Controllers
{
    // =====================================================================
    //  UC4 - Asignar personal a una sucursal
    //  Equipo: Mapache   |   Responsable: Alaitz Keyra Felipez Ledezma
    //  Tablas que usa: Trabajador, Usuario, Rol, Sucursal
    //  Solo el responsable modifica este archivo y la carpeta Views/AsignarPersonal/
    // =====================================================================
    [Area("Sucursales")]
    [Authorize(Roles = RolesSistema.Administrador)]
    public class AsignarPersonalController : Controller
    {
        private readonly ViveroDbContext _context;

        public AsignarPersonalController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Sucursales/AsignarPersonal
        public IActionResult Index()
        {
            // TODO (Alaitz Keyra Felipez Ledezma): implementar el escenario principal y las extensiones de UC4
            return View();
        }
    }
}
