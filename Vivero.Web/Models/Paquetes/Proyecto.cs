using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: Proyecto  |  Dueño: Equipo FRIENDS
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("Proyecto")]
    public class Proyecto
    {
        [Key]
        public int IdProyecto { get; set; }

        public int IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public Cliente? Cliente { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(120, ErrorMessage = "Máximo 120 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string? Descripcion { get; set; }

        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        public string? Ubicacion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Estado { get; set; } = "Abierto";

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
