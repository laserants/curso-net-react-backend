using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Curso_Backend_SEGEPLAN.Entities
{
    [Table("Actividades")]
    public class Actividad
    {
        [Key]
        [Column("Actividad_Id")]
        public int ActividadID { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column("Proyecto_Id")]
        public int ProyectoID { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "El campo {0} solo puede contener 50 caracteres")]
        public string Nombre { get; set; }


        [StringLength(250, ErrorMessage = "El campo {0} solo puede contener 250 caracteres")]
        public string Descripcion { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime FechaInicio { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime FechaFin { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "decimal(65,2)")]
        public decimal Costo { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "El campo {0} solo puede contener 100 caracteres")]
        public string NombreResponsable { get; set; }
    }
}
