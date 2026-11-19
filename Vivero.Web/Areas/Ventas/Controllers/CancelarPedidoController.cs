using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Ventas.Controllers
{
    // =====================================================================
    //  CU15 - Cancelar pedido
    //  Equipo: Dinamita   |   Responsable: Jhamil Flores Choque
    //  Tablas que usa: Pedido
    //  Solo el responsable modifica este archivo y la carpeta Views/CancelarPedido/
    // =====================================================================
    [Area("Ventas")]
    [Authorize(Roles = RolesSistema.Cliente)]
    public class CancelarPedidoController : Controller
    {
        private readonly ViveroDbContext _context;

        public CancelarPedidoController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Ventas/CancelarPedido
        public IActionResult Index()
        {
            // TODO (Jhamil Flores Choque): implementar el escenario principal y las extensiones de CU15
            return View();
        }
    }
}
