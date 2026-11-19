using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Areas.Inventario.Services;
using Vivero.Web.Data;
using Vivero.Web.Services;

// =====================================================================
//  PUNTO DE ENTRADA DEL SISTEMA DE VIVERO
//  ARCHIVO COMPARTIDO: no lo modifiques en tu rama.
// =====================================================================

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Cada integrante puede poner SU contraseña de MySQL en appsettings.Local.json
// (ese archivo NO se sube a Git, así no hay conflictos). Ver GUIA_EQUIPOS.md
builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);

// MVC
builder.Services.AddControllersWithViews();

// Base de datos MySQL con Entity Framework Core (Pomelo)
string? cadenaConexion = builder.Configuration.GetConnectionString("ViveroDb");
if (string.IsNullOrEmpty(cadenaConexion))
{
    throw new Exception("Falta la cadena de conexión 'ViveroDb' en appsettings.json o appsettings.Local.json");
}
// AutoDetect: funciona igual con MySQL 8 (Workbench) y con MariaDB (XAMPP).
// Por eso MySQL/XAMPP debe estar ENCENDIDO antes de ejecutar el proyecto.
ServerVersion versionServidor = ServerVersion.AutoDetect(cadenaConexion);
builder.Services.AddDbContext<ViveroDbContext>(opciones => opciones.UseMySql(cadenaConexion, versionServidor));

// Servicios compartidos
builder.Services.AddScoped<NotificacionService>();

// Proceso automático diario de Mapache UC11 (alertas de bajo stock)
builder.Services.AddHostedService<VerificadorStockBajoService>();

// Login con cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opciones =>
    {
        opciones.LoginPath = "/Cuenta/Login";
        opciones.LogoutPath = "/Cuenta/Logout";
        opciones.AccessDeniedPath = "/Cuenta/AccesoDenegado";
        opciones.ExpireTimeSpan = TimeSpan.FromHours(8);
    });
builder.Services.AddAuthorization();

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Rutas de los módulos (Areas): /Sucursales/..., /Inventario/..., /Personal/..., /Tareas/..., etc.
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Inicio}/{action=Index}/{id?}");

// Rutas generales: /Home, /Cuenta
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
