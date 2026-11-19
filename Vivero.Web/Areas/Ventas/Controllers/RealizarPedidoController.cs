using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Ventas.Controllers
{
    // =====================================================================
    //  CU14 - Realizar pedido en línea
    //  Equipo: Dinamita   |   Responsable: Jhamil Flores Choque
    //  Tablas que usa: Pedido, DetallePedido, Planta, Cliente
    //  Solo el responsable modifica este archivo y la carpeta Views/RealizarPedido/
    // =====================================================================
    [Area("Ventas")]
    [Authorize(Roles = RolesSistema.Cliente)]
    public class RealizarPedidoController : Controller
    {
        private readonly ViveroDbContext _context;

        public RealizarPedidoController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Ventas/RealizarPedido
        public IActionResult Index()
        {
            // TODO (Jhamil Flores Choque): implementar el escenario principal y las extensiones de CU14
            return View();
        }
    }
}
