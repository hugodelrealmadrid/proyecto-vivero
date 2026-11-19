using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: Cliente  |  Dueño: BASE COMÚN (solo la modifica la encargada de la BD)
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        [Display(Name = "CI / NIT")]
        public string CiNit { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(120, ErrorMessage = "Máximo 120 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string? Telefono { get; set; }

        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [EmailAddress(ErrorMessage = "Correo no válido")]
        public string? Correo { get; set; }

        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        public string? Direccion { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public bool Activo { get; set; } = true;
    }
}
