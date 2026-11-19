using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Entregas.Controllers
{
    // =====================================================================
    //  CU01 - Registrar entrega
    //  Equipo: Dinamita   |   Responsable: Franz Villarroel Jattaco
    //  Tablas que usa: Entrega, Pedido, Cliente, DireccionEntrega
    //  Solo el responsable modifica este archivo y la carpeta Views/RegistrarEntrega/
    // =====================================================================
    [Area("Entregas")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor)]
    public class RegistrarEntregaController : Controller
    {
        private readonly ViveroDbContext _context;

        public RegistrarEntregaController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Entregas/RegistrarEntrega
        public IActionResult Index()
        {
            // TODO (Franz Villarroel Jattaco): implementar el escenario principal y las extensiones de CU01
            return View();
        }
    }
}
