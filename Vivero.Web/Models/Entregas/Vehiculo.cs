using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: Vehiculo  |  Dueño: Equipo Dinamita
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("Vehiculo")]
    public class Vehiculo
    {
        [Key]
        public int IdVehiculo { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(15, ErrorMessage = "Máximo 15 caracteres")]
        public string Placa { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(40, ErrorMessage = "Máximo 40 caracteres")]
        public string Tipo { get; set; } = string.Empty;

        public bool Disponible { get; set; } = true;
    }
}
