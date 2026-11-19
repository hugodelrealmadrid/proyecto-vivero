using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: Proveedor  |  Dueño: Equipo Mapache
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("Proveedor")]
    public class Proveedor
    {
        [Key]
        public int IdProveedor { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(120, ErrorMessage = "Máximo 120 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [Display(Name = "Tipo de material")]
        public string TipoMaterial { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(120, ErrorMessage = "Máximo 120 caracteres")]
        public string Contacto { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(10, ErrorMessage = "Máximo 10 caracteres")]
        public string Estado { get; set; } = "Activo";

        public int IdUsuarioRegistro { get; set; }
        [ForeignKey("IdUsuarioRegistro")]
        public Usuario? UsuarioRegistro { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public DateTime? FechaBaja { get; set; }
    }
}
