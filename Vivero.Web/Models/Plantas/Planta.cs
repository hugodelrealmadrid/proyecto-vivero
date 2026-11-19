using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: Planta  |  Dueño: Equipo Code³
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("Planta")]
    public class Planta
    {
        [Key]
        public int IdPlanta { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Codigo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string? Especie { get; set; }

        [StringLength(60, ErrorMessage = "Máximo 60 caracteres")]
        public string? Tipo { get; set; }

        [StringLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string? Descripcion { get; set; }

        public bool EsLote { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "No se permiten valores negativos")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(30, ErrorMessage = "Máximo 30 caracteres")]
        public string EstadoPlanta { get; set; } = "Sana";

        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioVenta { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal CostoReferencial { get; set; }

        [StringLength(300, ErrorMessage = "Máximo 300 caracteres")]
        public string? ImagenUrl { get; set; }

        public int IdSucursal { get; set; }
        [ForeignKey("IdSucursal")]
        public Sucursal? Sucursal { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string EstadoRegistro { get; set; } = "Activo";

        public DateTime? FechaBaja { get; set; }

        [StringLength(250, ErrorMessage = "Máximo 250 caracteres")]
        public string? MotivoBaja { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
