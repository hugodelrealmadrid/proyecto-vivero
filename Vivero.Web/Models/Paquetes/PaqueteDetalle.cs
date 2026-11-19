using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: PaqueteDetalle  |  Dueño: Equipo FRIENDS
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("PaqueteDetalle")]
    public class PaqueteDetalle
    {
        [Key]
        public int IdPaqueteDetalle { get; set; }

        public int IdPaquete { get; set; }
        [ForeignKey("IdPaquete")]
        public Paquete? Paquete { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string TipoElemento { get; set; } = string.Empty;

        public int? IdPlanta { get; set; }
        [ForeignKey("IdPlanta")]
        public Planta? Planta { get; set; }

        public int? IdMaterial { get; set; }
        [ForeignKey("IdMaterial")]
        public Material? Material { get; set; }

        public int? IdServicio { get; set; }
        [ForeignKey("IdServicio")]
        public Servicio? Servicio { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Cantidad { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal CostoUnitario { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Subtotal { get; set; }
    }
}
