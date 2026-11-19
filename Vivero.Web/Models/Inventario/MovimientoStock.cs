using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: MovimientoStock  |  Dueño: Equipo Mapache
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("MovimientoStock")]
    public class MovimientoStock
    {
        [Key]
        public int IdMovimiento { get; set; }

        public int IdStockMaterial { get; set; }
        [ForeignKey("IdStockMaterial")]
        public StockMaterial? StockMaterial { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(30, ErrorMessage = "Máximo 30 caracteres")]
        public string Tipo { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Cantidad { get; set; }

        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string? Referencia { get; set; }

        public int? IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
