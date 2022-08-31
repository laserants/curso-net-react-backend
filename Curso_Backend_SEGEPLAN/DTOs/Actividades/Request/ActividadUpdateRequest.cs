using System.ComponentModel.DataAnnotations;

namespace Curso_Backend_SEGEPLAN.DTOs.Actividades.Request
{
    public class ActividadUpdateRequest : ActividadCreationRequest
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int ActividadID { get; set; }
    }
}
