using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Ventas.Controllers
{
    // =====================================================================
    //  CU12 - Registrar cliente
    //  Equipo: Dinamita   |   Responsable: Stif Lazo Barreto
    //  Tablas que usa: Cliente
    //  Solo el responsable modifica este archivo y la carpeta Views/RegistrarCliente/
    // =====================================================================
    [Area("Ventas")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor)]
    public class RegistrarClienteController : Controller
    {
        private readonly ViveroDbContext _context;

        public RegistrarClienteController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Ventas/RegistrarCliente
        public IActionResult Index()
        {
            // TODO (Stif Lazo Barreto): implementar el escenario principal y las extensiones de CU12
            return View();
        }
    }
}
