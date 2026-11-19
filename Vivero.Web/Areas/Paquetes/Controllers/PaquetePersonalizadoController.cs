using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Paquetes.Controllers
{
    // =====================================================================
    //  UC6.2 - Crear paquete personalizado
    //  Equipo: FRIENDS   |   Responsable: José Manuel Yanarico Quispe
    //  Tablas que usa: Paquete, PaqueteDetalle, Cliente, ReservaMaterial, StockMaterial, Cotizacion
    //  Solo el responsable modifica este archivo y la carpeta Views/PaquetePersonalizado/
    // =====================================================================
    [Area("Paquetes")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Disenador)]
    public class PaquetePersonalizadoController : Controller
    {
        private readonly ViveroDbContext _context;

        public PaquetePersonalizadoController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Paquetes/PaquetePersonalizado
        public IActionResult Index()
        {
            // TODO (José Manuel Yanarico Quispe): implementar el escenario principal y las extensiones de UC6.2
            return View();
        }
    }
}
