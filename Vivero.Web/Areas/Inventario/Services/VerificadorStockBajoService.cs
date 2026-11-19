using Vivero.Web.Data;

namespace Vivero.Web.Areas.Inventario.Services
{
    // =====================================================================
    //  UC11 - Notificar bajo stock de material (proceso AUTOMÁTICO)
    //  Equipo: Mapache   |   Responsable: Roberto Carlos Herrera Coca
    //
    //  Este servicio se ejecuta solo, en segundo plano, cada 24 horas
    //  (requisito: "al menos una vez al día, sin intervención manual").
    //  Ya está registrado en Program.cs, NO hace falta tocar Program.cs.
    //
    //  Solo el responsable modifica este archivo.
    // =====================================================================
    public class VerificadorStockBajoService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<VerificadorStockBajoService> _logger;

        public VerificadorStockBajoService(IServiceScopeFactory scopeFactory, ILogger<VerificadorStockBajoService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // El DbContext no se puede guardar en un servicio que vive siempre,
                    // por eso se crea uno nuevo en cada revisión.
                    using (IServiceScope scope = _scopeFactory.CreateScope())
                    {
                        ViveroDbContext context = scope.ServiceProvider.GetRequiredService<ViveroDbContext>();
                        await RevisarStockBajo(context);
                    }
                }
                catch (Exception error)
                {
                    _logger.LogError(error, "Error al verificar el stock bajo");
                }

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }

        // TODO (Roberto): implementar el escenario principal de UC11
        //  1. Buscar los StockMaterial con UmbralMinimo != null y Cantidad < UmbralMinimo
        //  2. Crear una AlertaStock por cada uno (si no tiene ya una alerta sin atender)
        //  3. Notificar al Supervisor de esa sucursal (NotificacionService)
        //  1a. Los que tienen UmbralMinimo == null NO generan alerta (pendientes de configurar, ver UC12)
        public static async Task RevisarStockBajo(ViveroDbContext context)
        {
            await Task.CompletedTask;
        }
    }
}
