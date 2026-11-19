using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: Servicio  |  Dueño: BASE COMÚN (solo la modifica la encargada de la BD)
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("Servicio")]
    public class Servicio
    {
        [Key]
        public int IdServicio { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "Máximo 300 caracteres")]
        public string? Descripcion { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal CostoReferencial { get; set; }

        public bool Activo { get; set; } = true;
    }
}
