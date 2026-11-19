using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Ventas.Controllers
{
    // =====================================================================
    //  CU17 - Consultar historial de compras
    //  Equipo: Dinamita   |   Responsable: Jhamil Flores Choque
    //  Tablas que usa: Pedido, DetallePedido, Planta, Cliente
    //  Solo el responsable modifica este archivo y la carpeta Views/HistorialCompras/
    // =====================================================================
    [Area("Ventas")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor + "," + RolesSistema.Cliente)]
    public class HistorialComprasController : Controller
    {
        private readonly ViveroDbContext _context;

        public HistorialComprasController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Ventas/HistorialCompras
        public IActionResult Index()
        {
            // TODO (Jhamil Flores Choque): implementar el escenario principal y las extensiones de CU17
            return View();
        }
    }
}
