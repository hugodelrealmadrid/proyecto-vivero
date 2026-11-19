using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vivero.Web.Data;
using Vivero.Web.Helpers;
using Vivero.Web.Models;
using Vivero.Web.ViewModels;

namespace Vivero.Web.Controllers
{
    // Inicio y cierre de sesión. ARCHIVO COMPARTIDO: no lo modifiques en tu rama.
    public class CuentaController : Controller
    {
        private readonly ViveroDbContext _context;

        public CuentaController(ViveroDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl)
        {
            LoginViewModel modelo = new LoginViewModel();
            modelo.ReturnUrl = returnUrl;
            return View(modelo);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            Usuario? usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .Include(u => u.Trabajador)
                .FirstOrDefaultAsync(u => u.NombreUsuario == modelo.NombreUsuario);

            if (usuario == null || !usuario.Activo)
            {
                ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos.");
                return View(modelo);
            }

            PasswordHasher<Usuario> hasher = new PasswordHasher<Usuario>();
            PasswordVerificationResult resultado = hasher.VerifyHashedPassword(usuario, usuario.PasswordHash, modelo.Password);
            if (resultado == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos.");
                return View(modelo);
            }

            // Datos que se guardan en la cookie (se leen con UsuarioActual.*)
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, usuario.NombreUsuario));
            claims.Add(new Claim(ClaimTypes.Role, usuario.Rol!.Nombre));
            claims.Add(new Claim(UsuarioActual.ClaimIdUsuario, usuario.IdUsuario.ToString()));
            if (usuario.IdTrabajador != null)
            {
                claims.Add(new Claim(UsuarioActual.ClaimIdTrabajador, usuario.IdTrabajador.Value.ToString()));
            }
            if (usuario.Trabajador != null && usuario.Trabajador.IdSucursal != null)
            {
                claims.Add(new Claim(UsuarioActual.ClaimIdSucursal, usuario.Trabajador.IdSucursal.Value.ToString()));
            }
            if (usuario.IdCliente != null)
            {
                claims.Add(new Claim(UsuarioActual.ClaimIdCliente, usuario.IdCliente.Value.ToString()));
            }

            ClaimsIdentity identidad = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identidad);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            if (!string.IsNullOrEmpty(modelo.ReturnUrl) && Url.IsLocalUrl(modelo.ReturnUrl))
            {
                return Redirect(modelo.ReturnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccesoDenegado()
        {
            return View();
        }
    }
}
