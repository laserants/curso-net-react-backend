using System.ComponentModel.DataAnnotations;

namespace Curso_Backend_SEGEPLAN.DTOs.ProyectosBeneficiarios.Request
{
    public class ProyectoBeneficiarioCreationRequest
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int ProyectoID { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int BeneficiarioID { get; set; }
    }
}
