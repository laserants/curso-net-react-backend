using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Curso_Backend_SEGEPLAN.Entities
{
    [Table("Beneficiarios")]
    public class Beneficiario
    {
        [Key]
        [Column("Beneficiario_Id")]
        public int BeneficiarioID { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(150, ErrorMessage = "El campo {0} solo puede contener 150 caracteres")]
        public string Nombre { get; set; }

        [StringLength(250, ErrorMessage = "El campo {0} solo puede contener 250 caracteres")]
        public string Descripcion { get; set; }
    }
}
