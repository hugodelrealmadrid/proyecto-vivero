using System.ComponentModel.DataAnnotations;

namespace Vivero.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Ingresa tu usuario")]
        [Display(Name = "Usuario")]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingresa tu contraseña")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; } = string.Empty;

        public string? ReturnUrl { get; set; }
    }
}
