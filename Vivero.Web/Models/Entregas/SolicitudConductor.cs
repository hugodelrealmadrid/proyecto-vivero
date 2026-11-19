using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: SolicitudConductor  |  Dueño: Equipo Dinamita
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("SolicitudConductor")]
    public class SolicitudConductor
    {
        [Key]
        public int IdSolicitud { get; set; }

        public int IdEntrega { get; set; }
        [ForeignKey("IdEntrega")]
        public Entrega? Entrega { get; set; }

        public int IdConductor { get; set; }
        [ForeignKey("IdConductor")]
        public Conductor? Conductor { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Estado { get; set; } = "Pendiente";

        public DateTime FechaHora { get; set; } = DateTime.Now;
    }
}
