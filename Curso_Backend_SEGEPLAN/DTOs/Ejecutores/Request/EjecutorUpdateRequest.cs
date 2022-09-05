using System.ComponentModel.DataAnnotations;

namespace Curso_Backend_SEGEPLAN.DTOs.Ejecutores.Request
{
    public class EjecutorUpdateRequest : EjecutorCreationRequest
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int EjecutorID { get; set; }
    }
}
