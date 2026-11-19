using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: OrdenCompra  |  Dueño: Equipo Code³
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("OrdenCompra")]
    public class OrdenCompra
    {
        [Key]
        public int IdOrdenCompra { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Codigo { get; set; } = string.Empty;

        public DateTime FechaGeneracion { get; set; } = DateTime.Now;

        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }

        public int IdSucursal { get; set; }
        [ForeignKey("IdSucursal")]
        public Sucursal? Sucursal { get; set; }

        public int? IdProveedor { get; set; }
        [ForeignKey("IdProveedor")]
        public Proveedor? Proveedor { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Estado { get; set; } = "Pendiente";

        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        [StringLength(300, ErrorMessage = "Máximo 300 caracteres")]
        public string? Observaciones { get; set; }

        public List<DetalleOrdenCompra> Detalles { get; set; } = new List<DetalleOrdenCompra>();
    }
}
