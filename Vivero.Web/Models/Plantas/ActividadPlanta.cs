using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: ActividadPlanta  |  Dueño: Equipo Code³
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("ActividadPlanta")]
    public class ActividadPlanta
    {
        [Key]
        public int IdActividad { get; set; }

        public int IdPlanta { get; set; }
        [ForeignKey("IdPlanta")]
        public Planta? Planta { get; set; }

        public int IdTipoActividad { get; set; }
        [ForeignKey("IdTipoActividad")]
        public TipoActividad? TipoActividad { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Clase { get; set; } = "Actividad";

        [DataType(DataType.Date)]
        public DateTime FechaActividad { get; set; }

        public int IdTrabajador { get; set; }
        [ForeignKey("IdTrabajador")]
        public Trabajador? Responsable { get; set; }

        [StringLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string? Observaciones { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ProximaFecha { get; set; }

        public int IdUsuarioRegistro { get; set; }
        [ForeignKey("IdUsuarioRegistro")]
        public Usuario? UsuarioRegistro { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
