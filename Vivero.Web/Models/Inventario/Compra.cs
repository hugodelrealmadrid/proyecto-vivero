using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: Compra  |  Dueño: Equipo Mapache
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("Compra")]
    public class Compra
    {
        [Key]
        public int IdCompra { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Comprobante { get; set; } = string.Empty;

        public DateTime FechaCompra { get; set; } = DateTime.Now;

        public int IdProveedor { get; set; }
        [ForeignKey("IdProveedor")]
        public Proveedor? Proveedor { get; set; }

        public int IdSucursal { get; set; }
        [ForeignKey("IdSucursal")]
        public Sucursal? Sucursal { get; set; }

        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }

        public int? IdOrdenCompra { get; set; }
        [ForeignKey("IdOrdenCompra")]
        public OrdenCompra? OrdenCompra { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(15, ErrorMessage = "Máximo 15 caracteres")]
        public string Estado { get; set; } = "Registrada";

        public List<DetalleCompra> Detalles { get; set; } = new List<DetalleCompra>();
    }
}
