using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: Usuario  |  Dueño: BASE COMÚN (solo la modifica la encargada de la BD)
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string NombreUsuario { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string PasswordHash { get; set; } = string.Empty;

        public int IdRol { get; set; }
        [ForeignKey("IdRol")]
        public Rol? Rol { get; set; }

        public int? IdTrabajador { get; set; }
        [ForeignKey("IdTrabajador")]
        public Trabajador? Trabajador { get; set; }

        public int? IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public Cliente? Cliente { get; set; }

        public bool Activo { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
