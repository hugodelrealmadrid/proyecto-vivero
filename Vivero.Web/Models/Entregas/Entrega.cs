using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: Entrega  |  Dueño: Equipo Dinamita
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("Entrega")]
    public class Entrega
    {
        [Key]
        public int IdEntrega { get; set; }

        public int IdPedido { get; set; }
        [ForeignKey("IdPedido")]
        public Pedido? Pedido { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Codigo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Estado { get; set; } = "Pendiente";

        public DateTime FechaHora { get; set; }

        [StringLength(300, ErrorMessage = "Máximo 300 caracteres")]
        public string? Foto { get; set; }

        public int IdDireccionEntrega { get; set; }
        [ForeignKey("IdDireccionEntrega")]
        public DireccionEntrega? DireccionEntrega { get; set; }

        public int? IdVehiculo { get; set; }
        [ForeignKey("IdVehiculo")]
        public Vehiculo? Vehiculo { get; set; }

        public int? IdConductor { get; set; }
        [ForeignKey("IdConductor")]
        public Conductor? Conductor { get; set; }
    }
}
