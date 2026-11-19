using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: Cotizacion  |  Dueño: Equipo FRIENDS
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("Cotizacion")]
    public class Cotizacion
    {
        [Key]
        public int IdCotizacion { get; set; }

        public int IdPaquete { get; set; }
        [ForeignKey("IdPaquete")]
        public Paquete? Paquete { get; set; }

        public int IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public Cliente? Cliente { get; set; }

        public int NumeroVersion { get; set; } = 1;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Precio { get; set; }

        public string? Detalle { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Estado { get; set; } = "Vigente";

        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
