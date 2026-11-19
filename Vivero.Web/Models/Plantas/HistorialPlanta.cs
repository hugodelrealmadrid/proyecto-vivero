using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: HistorialPlanta  |  Dueño: Equipo Code³
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("HistorialPlanta")]
    public class HistorialPlanta
    {
        [Key]
        public int IdHistorialPlanta { get; set; }

        public int IdPlanta { get; set; }
        [ForeignKey("IdPlanta")]
        public Planta? Planta { get; set; }

        public int CantidadAnterior { get; set; }

        public int CantidadNueva { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(30, ErrorMessage = "Máximo 30 caracteres")]
        public string EstadoAnterior { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(30, ErrorMessage = "Máximo 30 caracteres")]
        public string EstadoNuevo { get; set; } = string.Empty;

        [StringLength(250, ErrorMessage = "Máximo 250 caracteres")]
        public string? Motivo { get; set; }

        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
