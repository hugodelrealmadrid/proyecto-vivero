using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: Reclamo  |  Dueño: Equipo Dinamita
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("Reclamo")]
    public class Reclamo
    {
        [Key]
        public int IdReclamo { get; set; }

        public int IdEntrega { get; set; }
        [ForeignKey("IdEntrega")]
        public Entrega? Entrega { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string Descripcion { get; set; } = string.Empty;

        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
