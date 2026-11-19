using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: Incidencia  |  Dueño: Equipo Dinamita
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("Incidencia")]
    public class Incidencia
    {
        [Key]
        public int IdIncidencia { get; set; }

        public int IdEntrega { get; set; }
        [ForeignKey("IdEntrega")]
        public Entrega? Entrega { get; set; }

        public int? IdConductor { get; set; }
        [ForeignKey("IdConductor")]
        public Conductor? Conductor { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(300, ErrorMessage = "Máximo 300 caracteres")]
        public string Motivo { get; set; } = string.Empty;

        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
