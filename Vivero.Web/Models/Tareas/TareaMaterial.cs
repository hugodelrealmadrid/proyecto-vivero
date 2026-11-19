using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Web.Models
{
    // Tabla: TareaMaterial  |  Dueño: Equipo FRIENDS
    // No cambies nombres ni tipos: deben coincidir con database/vivero_db.sql
    [Table("TareaMaterial")]
    public class TareaMaterial
    {
        [Key]
        public int IdTareaMaterial { get; set; }

        public int IdTarea { get; set; }
        [ForeignKey("IdTarea")]
        public Tarea? Tarea { get; set; }

        public int IdMaterial { get; set; }
        [ForeignKey("IdMaterial")]
        public Material? Material { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal CantidadRequerida { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal CantidadUsada { get; set; }
    }
}
