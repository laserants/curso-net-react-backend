using System.ComponentModel.DataAnnotations;

namespace Curso_Backend_SEGEPLAN.DTOs.Beneficiarios.Request
{
    public class BeneficiarioCreationRequest
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(150, ErrorMessage = "El campo {0} solo puede contener 150 caracteres")]
        public string Nombre { get; set; }

        [StringLength(250, ErrorMessage = "El campo {0} solo puede contener 250 caracteres")]
        public string Descripcion { get; set; }
    }
}
