using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: Venta  |  Dueño: Equipo Dinamita
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("Venta")]
    public class Venta
    {
        [Key]
        public int IdVenta { get; set; }

        public DateTime FechaVenta { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        public int IdSucursal { get; set; }
        [ForeignKey("IdSucursal")]
        public Sucursal? Sucursal { get; set; }

        public int? IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public Cliente? Cliente { get; set; }

        public int IdSupervisor { get; set; }
        [ForeignKey("IdSupervisor")]
        public Trabajador? Supervisor { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Estado { get; set; } = "Registrada";

        public List<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
    }
}
