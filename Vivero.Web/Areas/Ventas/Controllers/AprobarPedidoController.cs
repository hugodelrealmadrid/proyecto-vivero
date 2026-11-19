using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Ventas.Controllers
{
    // =====================================================================
    //  CU18 - Revisar y aprobar pedido en línea
    //  Equipo: Dinamita   |   Responsable: Jhamil Flores Choque
    //  Tablas que usa: Pedido, DetallePedido, Planta
    //  Solo el responsable modifica este archivo y la carpeta Views/AprobarPedido/
    // =====================================================================
    [Area("Ventas")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor)]
    public class AprobarPedidoController : Controller
    {
        private readonly ViveroDbContext _context;

        public AprobarPedidoController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Ventas/AprobarPedido
        public IActionResult Index()
        {
            // TODO (Jhamil Flores Choque): implementar el escenario principal y las extensiones de CU18
            return View();
        }
    }
}
