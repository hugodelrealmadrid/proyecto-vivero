using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: Notificacion  |  Dueño: BASE COMÚN (solo la modifica la encargada de la BD)
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("Notificacion")]
    public class Notificacion
    {
        [Key]
        public int IdNotificacion { get; set; }

        public int IdUsuarioDestino { get; set; }
        [ForeignKey("IdUsuarioDestino")]
        public Usuario? UsuarioDestino { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(120, ErrorMessage = "Máximo 120 caracteres")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string Mensaje { get; set; } = string.Empty;

        public bool Leida { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
