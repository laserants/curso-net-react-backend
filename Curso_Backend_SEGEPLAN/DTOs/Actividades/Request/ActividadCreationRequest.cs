using System.ComponentModel.DataAnnotations;

namespace Curso_Backend_SEGEPLAN.DTOs.Actividades.Request
{
    public class ActividadCreationRequest
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
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
        public decimal Costo { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "El campo {0} solo puede contener 100 caracteres")]
        public string NombreResponsable { get; set; }
    }
}
