using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Ventas.Controllers
{
    // =====================================================================
    //  CU09 - Registrar venta en vivero
    //  Equipo: Dinamita   |   Responsable: Stif Lazo Barreto
    //  Tablas que usa: Venta, DetalleVenta, Planta, Cliente
    //  Solo el responsable modifica este archivo y la carpeta Views/RegistrarVenta/
    // =====================================================================
    [Area("Ventas")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor)]
    public class RegistrarVentaController : Controller
    {
        private readonly ViveroDbContext _context;

        public RegistrarVentaController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Ventas/RegistrarVenta
        public IActionResult Index()
        {
            // TODO (Stif Lazo Barreto): implementar el escenario principal y las extensiones de CU09
            return View();
        }
    }
}
