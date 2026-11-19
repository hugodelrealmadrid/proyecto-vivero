using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Paquetes.Controllers
{
    // =====================================================================
    //  UC6.6 - Asignar paquete a proyecto / cliente
    //  Equipo: FRIENDS   |   Responsable: José Manuel Yanarico Quispe
    //  Tablas que usa: AsignacionPaquete, Paquete, Cliente, Proyecto, ReservaMaterial, StockMaterial, Cotizacion
    //  Solo el responsable modifica este archivo y la carpeta Views/AsignarPaquete/
    // =====================================================================
    [Area("Paquetes")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Disenador)]
    public class AsignarPaqueteController : Controller
    {
        private readonly ViveroDbContext _context;

        public AsignarPaqueteController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Paquetes/AsignarPaquete
        public IActionResult Index()
        {
            // TODO (José Manuel Yanarico Quispe): implementar el escenario principal y las extensiones de UC6.6
            return View();
        }
    }
}
