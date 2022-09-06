using System.ComponentModel.DataAnnotations;

namespace Curso_Backend_SEGEPLAN.DTOs.Proyectos.Request
{
    public class ProyectoUpdateRequest : ProyectoCreationRequest
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int ProyectoID { get; set; }
    }
}
