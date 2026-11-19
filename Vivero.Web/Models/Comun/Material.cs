using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: Material  |  Dueño: BASE COMÚN (solo la modifica la encargada de la BD)
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("Material")]
    public class Material
    {
        [Key]
        public int IdMaterial { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(60, ErrorMessage = "Máximo 60 caracteres")]
        public string? Tipo { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Unidad { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal CostoUnitario { get; set; }

        public bool Activo { get; set; } = true;
    }
}
