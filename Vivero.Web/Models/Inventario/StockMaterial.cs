using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: StockMaterial  |  Dueño: Equipo Mapache
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("StockMaterial")]
    public class StockMaterial
    {
        [Key]
        public int IdStockMaterial { get; set; }

        public int IdMaterial { get; set; }
        [ForeignKey("IdMaterial")]
        public Material? Material { get; set; }

        public int IdSucursal { get; set; }
        [ForeignKey("IdSucursal")]
        public Sucursal? Sucursal { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Cantidad { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal CantidadReservada { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? UmbralMinimo { get; set; }
    }
}
