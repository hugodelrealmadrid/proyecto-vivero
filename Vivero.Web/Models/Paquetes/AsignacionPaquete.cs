using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: AsignacionPaquete  |  Dueño: Equipo FRIENDS
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("AsignacionPaquete")]
    public class AsignacionPaquete
    {
        [Key]
        public int IdAsignacion { get; set; }

        public int IdPaquete { get; set; }
        [ForeignKey("IdPaquete")]
        public Paquete? Paquete { get; set; }

        public int IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public Cliente? Cliente { get; set; }

        public int? IdProyecto { get; set; }
        [ForeignKey("IdProyecto")]
        public Proyecto? Proyecto { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaServicio { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        public string Ubicacion { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Máximo 1000 caracteres")]
        public string? Condiciones { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioCotizado { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Estado { get; set; } = "Cotizado";

        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }

        public DateTime FechaAsignacion { get; set; } = DateTime.Now;

        public DateTime? FechaAceptacion { get; set; }

        public int? IdUsuarioAceptacion { get; set; }
        [ForeignKey("IdUsuarioAceptacion")]
        public Usuario? UsuarioAceptacion { get; set; }

        [StringLength(300, ErrorMessage = "Máximo 300 caracteres")]
        public string? EvidenciaAceptacion { get; set; }
    }
}
