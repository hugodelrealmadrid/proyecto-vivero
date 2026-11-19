using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: PaqueteImagen  |  Dueño: Equipo FRIENDS
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("PaqueteImagen")]
    public class PaqueteImagen
    {
        [Key]
        public int IdPaqueteImagen { get; set; }

        public int IdPaquete { get; set; }
        [ForeignKey("IdPaquete")]
        public Paquete? Paquete { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(300, ErrorMessage = "Máximo 300 caracteres")]
        public string Url { get; set; } = string.Empty;
    }
}
