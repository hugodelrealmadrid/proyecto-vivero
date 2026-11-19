using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: Pedido  |  Dueño: Equipo Dinamita
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("Pedido")]
    public class Pedido
    {
        [Key]
        public int IdPedido { get; set; }

        public DateTime FechaPedido { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Estado { get; set; } = "Pendiente";

        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        public int IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public Cliente? Cliente { get; set; }

        public int IdSucursal { get; set; }
        [ForeignKey("IdSucursal")]
        public Sucursal? Sucursal { get; set; }

        public int? IdSupervisor { get; set; }
        [ForeignKey("IdSupervisor")]
        public Trabajador? Supervisor { get; set; }

        public DateTime? FechaAprobacion { get; set; }

        [StringLength(250, ErrorMessage = "Máximo 250 caracteres")]
        public string? MotivoCancelacion { get; set; }

        public List<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();
    }
}
