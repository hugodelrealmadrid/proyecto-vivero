using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: Conductor  |  Dueño: Equipo Dinamita
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("Conductor")]
    public class Conductor
    {
        [Key]
        public int IdConductor { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(120, ErrorMessage = "Máximo 120 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string? Telefono { get; set; }

        [StringLength(30, ErrorMessage = "Máximo 30 caracteres")]
        public string? Licencia { get; set; }

        public bool Disponible { get; set; } = true;

        public int? IdTrabajador { get; set; }
        [ForeignKey("IdTrabajador")]
        public Trabajador? Trabajador { get; set; }
    }
}
