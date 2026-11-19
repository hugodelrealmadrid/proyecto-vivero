using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Entregas.Controllers
{
    // =====================================================================
    //  CU07 - Confirmar recepción de pedido
    //  Equipo: Dinamita   |   Responsable: Jhery Rodriguez Aguilar
    //  Tablas que usa: Entrega, Pedido, Reclamo
    //  Solo el responsable modifica este archivo y la carpeta Views/ConfirmarRecepcion/
    // =====================================================================
    [Area("Entregas")]
    [Authorize(Roles = RolesSistema.Cliente)]
    public class ConfirmarRecepcionController : Controller
    {
        private readonly ViveroDbContext _context;

        public ConfirmarRecepcionController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Entregas/ConfirmarRecepcion
        public IActionResult Index()
        {
            // TODO (Jhery Rodriguez Aguilar): implementar el escenario principal y las extensiones de CU07
            return View();
        }
    }
}
