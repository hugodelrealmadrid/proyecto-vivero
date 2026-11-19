using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: TipoActividad  |  Dueño: Equipo Code³
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("TipoActividad")]
    public class TipoActividad
    {
        [Key]
        public int IdTipoActividad { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(60, ErrorMessage = "Máximo 60 caracteres")]
        public string Nombre { get; set; } = string.Empty;
    }
}
