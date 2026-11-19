using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: Paquete  |  Dueño: Equipo FRIENDS
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("Paquete")]
    public class Paquete
    {
        [Key]
        public int IdPaquete { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Codigo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(120, ErrorMessage = "Máximo 120 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(60, ErrorMessage = "Máximo 60 caracteres")]
        [Display(Name = "Tipo de evento")]
        public string TipoEvento { get; set; } = string.Empty;

        [StringLength(60, ErrorMessage = "Máximo 60 caracteres")]
        public string? Estilo { get; set; }

        [StringLength(1000, ErrorMessage = "Máximo 1000 caracteres")]
        public string? Descripcion { get; set; }

        public bool EsPersonalizado { get; set; }

        public int? IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public Cliente? Cliente { get; set; }

        public int? IdSucursal { get; set; }
        [ForeignKey("IdSucursal")]
        public Sucursal? Sucursal { get; set; }

        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        public string? Ubicacion { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FechaPrevista { get; set; }

        [StringLength(1000, ErrorMessage = "Máximo 1000 caracteres")]
        public string? Necesidades { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal CostoReferencial { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Estado { get; set; } = "Disponible";

        public int IdUsuarioCreador { get; set; }
        [ForeignKey("IdUsuarioCreador")]
        public Usuario? UsuarioCreador { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public DateTime? FechaModificacion { get; set; }

        [ConcurrencyCheck]
        public int Version { get; set; } = 1;

        public List<PaqueteDetalle> Detalles { get; set; } = new List<PaqueteDetalle>();

        public List<PaqueteImagen> Imagenes { get; set; } = new List<PaqueteImagen>();
    }
}
