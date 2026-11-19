using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: Tarea  |  Dueño: Equipo FRIENDS
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("Tarea")]
    public class Tarea
    {
        [Key]
        public int IdTarea { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(60, ErrorMessage = "Máximo 60 caracteres")]
        [Display(Name = "Tipo de trabajo")]
        public string TipoTrabajo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        public string Ubicacion { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime FechaProgramada { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(10, ErrorMessage = "Máximo 10 caracteres")]
        public string Prioridad { get; set; } = "Media";

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(30, ErrorMessage = "Máximo 30 caracteres")]
        public string Estado { get; set; } = "Pendiente";

        public int? IdTrabajador { get; set; }
        [ForeignKey("IdTrabajador")]
        public Trabajador? Trabajador { get; set; }

        public int? IdEspecialidadRequerida { get; set; }
        [ForeignKey("IdEspecialidadRequerida")]
        public Especialidad? EspecialidadRequerida { get; set; }

        public int IdSucursal { get; set; }
        [ForeignKey("IdSucursal")]
        public Sucursal? Sucursal { get; set; }

        public int? IdPaquete { get; set; }
        [ForeignKey("IdPaquete")]
        public Paquete? Paquete { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public int IdUsuarioCreador { get; set; }
        [ForeignKey("IdUsuarioCreador")]
        public Usuario? UsuarioCreador { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [ConcurrencyCheck]
        public int Version { get; set; } = 1;

        public List<TareaMaterial> Materiales { get; set; } = new List<TareaMaterial>();

        public List<HistorialTarea> Historial { get; set; } = new List<HistorialTarea>();
    }
}
