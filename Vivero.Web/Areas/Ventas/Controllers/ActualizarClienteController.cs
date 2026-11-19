using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Ventas.Controllers
{
    // =====================================================================
    //  CU13 - Actualizar datos de cliente
    //  Equipo: Dinamita   |   Responsable: Stif Lazo Barreto
    //  Tablas que usa: Cliente
    //  Solo el responsable modifica este archivo y la carpeta Views/ActualizarCliente/
    // =====================================================================
    [Area("Ventas")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor)]
    public class ActualizarClienteController : Controller
    {
        private readonly ViveroDbContext _context;

        public ActualizarClienteController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Ventas/ActualizarCliente
        public IActionResult Index()
        {
            // TODO (Stif Lazo Barreto): implementar el escenario principal y las extensiones de CU13
            return View();
        }
    }
}
