using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: Trabajador  |  Dueño: Equipo Code³
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("Trabajador")]
    public class Trabajador
    {
        [Key]
        public int IdTrabajador { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string CI { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(80, ErrorMessage = "Máximo 80 caracteres")]
        public string Nombres { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(80, ErrorMessage = "Máximo 80 caracteres")]
        public string Apellidos { get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string? Telefono { get; set; }

        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [EmailAddress(ErrorMessage = "Correo no válido")]
        public string? Correo { get; set; }

        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        public string? Direccion { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; }

        public int IdPuesto { get; set; }
        [ForeignKey("IdPuesto")]
        public Puesto? Puesto { get; set; }

        public int? IdSucursal { get; set; }
        [ForeignKey("IdSucursal")]
        public Sucursal? Sucursal { get; set; }

        public int? IdEspecialidad { get; set; }
        [ForeignKey("IdEspecialidad")]
        public Especialidad? Especialidad { get; set; }

        public bool DisponibleReasignacion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Estado { get; set; } = "Activo";

        public DateTime? FechaBaja { get; set; }

        [StringLength(250, ErrorMessage = "Máximo 250 caracteres")]
        public string? MotivoBaja { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public DateTime? FechaActualizacion { get; set; }
    }
}
