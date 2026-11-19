using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Plantas.Controllers
{
    // =====================================================================
    //  CU08 - Generar orden de compra de suministros
    //  Equipo: Code³   |   Responsable: Ariel Rodrigo Rocabado
    //  Tablas que usa: OrdenCompra, DetalleOrdenCompra, Material, Proveedor
    //  Solo el responsable modifica este archivo y la carpeta Views/OrdenCompra/
    // =====================================================================
    [Area("Plantas")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor)]
    public class OrdenCompraController : Controller
    {
        private readonly ViveroDbContext _context;

        public OrdenCompraController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Plantas/OrdenCompra
        public IActionResult Index()
        {
            // TODO (Ariel Rodrigo Rocabado): implementar el escenario principal y las extensiones de CU08
            return View();
        }
    }
}
