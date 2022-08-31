using System.ComponentModel.DataAnnotations;

namespace Curso_Backend_SEGEPLAN.DTOs.Proyectos.Request
{
    public class ProyectoCreationRequest
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "El campo {0} solo puede contener 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime FechaFin { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal Presupuesto { get; set; }

        [StringLength(150, ErrorMessage = "El campo {0} solo puede contener 150 caracteres")]
        public string Alcance { get; set; }
    }
}
