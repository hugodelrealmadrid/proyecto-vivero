using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Inventario.Controllers
{
    // =====================================================================
    //  UC8 - Registrar compra de material
    //  Equipo: Mapache   |   Responsable: Yubidsa Gabriela Bueno Cocarico
    //  Tablas que usa: Compra, DetalleCompra, Proveedor, StockMaterial, MovimientoStock, OrdenCompra
    //  Solo el responsable modifica este archivo y la carpeta Views/RegistrarCompra/
    // =====================================================================
    [Area("Inventario")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor)]
    public class RegistrarCompraController : Controller
    {
        private readonly ViveroDbContext _context;

        public RegistrarCompraController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Inventario/RegistrarCompra
        public IActionResult Index()
        {
            // TODO (Yubidsa Gabriela Bueno Cocarico): implementar el escenario principal y las extensiones de UC8
            return View();
        }
    }
}
