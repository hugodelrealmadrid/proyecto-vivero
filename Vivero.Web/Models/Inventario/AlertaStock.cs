using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: AlertaStock  |  Dueño: Equipo Mapache
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("AlertaStock")]
    public class AlertaStock
    {
        [Key]
        public int IdAlerta { get; set; }

        public int IdStockMaterial { get; set; }
        [ForeignKey("IdStockMaterial")]
        public StockMaterial? StockMaterial { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(300, ErrorMessage = "Máximo 300 caracteres")]
        public string Mensaje { get; set; } = string.Empty;

        public bool Atendida { get; set; }
    }
}
