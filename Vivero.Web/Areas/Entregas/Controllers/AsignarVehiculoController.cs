using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;

namespace Vivero.Web.Areas.Entregas.Controllers
{
    // =====================================================================
    //  CU02 - Asignar vehículo
    //  Equipo: Dinamita   |   Responsable: Franz Villarroel Jattaco
    //  Tablas que usa: Entrega, Vehiculo
    //  Solo el responsable modifica este archivo y la carpeta Views/AsignarVehiculo/
    // =====================================================================
    [Area("Entregas")]
    [Authorize(Roles = RolesSistema.Administrador + "," + RolesSistema.Supervisor)]
    public class AsignarVehiculoController : Controller
    {
        private readonly ViveroDbContext _context;

        public AsignarVehiculoController(ViveroDbContext context)
        {
            _context = context;
        }

        // GET: /Entregas/AsignarVehiculo
        public IActionResult Index()
        {
            // TODO (Franz Villarroel Jattaco): implementar el escenario principal y las extensiones de CU02
            return View();
        }
    }
}
