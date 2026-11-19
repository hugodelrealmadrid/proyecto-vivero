using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: Puesto  |  Dueño: BASE COMÚN (solo la modifica la encargada de la BD)
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("Puesto")]
    public class Puesto
    {
        [Key]
        public int IdPuesto { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(60, ErrorMessage = "Máximo 60 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        public string? Descripcion { get; set; }

        public bool Activo { get; set; } = true;
    }
}
