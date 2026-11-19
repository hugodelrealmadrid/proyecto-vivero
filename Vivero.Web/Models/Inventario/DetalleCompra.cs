using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: DetalleCompra  |  Dueño: Equipo Mapache
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("DetalleCompra")]
    public class DetalleCompra
    {
        [Key]
        public int IdDetalleCompra { get; set; }

        public int IdCompra { get; set; }
        [ForeignKey("IdCompra")]
        public Compra? Compra { get; set; }

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
