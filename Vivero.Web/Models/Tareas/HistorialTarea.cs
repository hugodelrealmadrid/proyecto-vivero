using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: HistorialTarea  |  Dueño: Equipo FRIENDS
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("HistorialTarea")]
    public class HistorialTarea
    {
        [Key]
        public int IdHistorialTarea { get; set; }

        public int IdTarea { get; set; }
        [ForeignKey("IdTarea")]
        public Tarea? Tarea { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(40, ErrorMessage = "Máximo 40 caracteres")]
        public string Accion { get; set; } = string.Empty;

        [StringLength(30, ErrorMessage = "Máximo 30 caracteres")]
        public string? EstadoAnterior { get; set; }

        [StringLength(30, ErrorMessage = "Máximo 30 caracteres")]
        public string? EstadoNuevo { get; set; }

        [StringLength(300, ErrorMessage = "Máximo 300 caracteres")]
        public string? Motivo { get; set; }

        public string? ValoresAnteriores { get; set; }

        public string? ValoresNuevos { get; set; }

        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
