using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: Sucursal  |  Dueño: BASE COMÚN (solo la modifica la encargada de la BD)
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("Sucursal")]
    public class Sucursal
    {
        [Key]
        public int IdSucursal { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(10, ErrorMessage = "Máximo 10 caracteres")]
        public string Codigo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        public string Direccion { get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(10, ErrorMessage = "Máximo 10 caracteres")]
        public string Estado { get; set; } = "Activa";

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public DateTime? FechaBaja { get; set; }
    }
}
