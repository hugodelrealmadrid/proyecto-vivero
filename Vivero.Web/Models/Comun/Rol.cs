using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: Rol  |  Dueño: BASE COMÚN (solo la modifica la encargada de la BD)
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("Rol")]
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(30, ErrorMessage = "Máximo 30 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        public string? Descripcion { get; set; }
    }
}
