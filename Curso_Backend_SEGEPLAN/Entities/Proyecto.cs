using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Curso_Backend_SEGEPLAN.Entities
{
    [Table("Proyectos")]
    public class Proyecto
    {
        [Key]
        [Column("Proyecto_Id")]
        public int ProyectoID { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "El campo {0} solo puede contener 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime FechaFin { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "decimal(65,2)")]
        public decimal Presupuesto { get; set; }

        [StringLength(150, ErrorMessage = "El campo {0} solo puede contener 150 caracteres")]
        public string Alcance { get; set; }
    }
}
