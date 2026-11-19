using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: DetalleOrdenCompra  |  Dueño: Equipo Code³
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("DetalleOrdenCompra")]
    public class DetalleOrdenCompra
    {
        [Key]
        public int IdDetalleOrdenCompra { get; set; }

        public int IdOrdenCompra { get; set; }
        [ForeignKey("IdOrdenCompra")]
        public OrdenCompra? OrdenCompra { get; set; }

        public int IdMaterial { get; set; }
        [ForeignKey("IdMaterial")]
        public Material? Material { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Cantidad { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioUnitario { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Subtotal { get; set; }
    }
}
