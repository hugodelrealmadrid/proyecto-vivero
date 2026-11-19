using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: DireccionEntrega  |  Dueño: Equipo Dinamita
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("DireccionEntrega")]
    public class DireccionEntrega
    {
        [Key]
        public int IdDireccionEntrega { get; set; }

        public int IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public Cliente? Cliente { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        public string Direccion { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        public string? Referencia { get; set; }
    }
}
