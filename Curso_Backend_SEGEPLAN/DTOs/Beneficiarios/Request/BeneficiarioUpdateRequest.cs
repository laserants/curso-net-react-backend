using System.ComponentModel.DataAnnotations;

namespace Curso_Backend_SEGEPLAN.DTOs.Beneficiarios.Request
{
    public class BeneficiarioUpdateRequest : BeneficiarioCreationRequest
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int BeneficiarioID { get; set; }
    }
}
