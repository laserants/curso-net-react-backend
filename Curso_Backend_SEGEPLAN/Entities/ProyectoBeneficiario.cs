using System.ComponentModel.DataAnnotations.Schema;

namespace Curso_Backend_SEGEPLAN.Entities
{
    [Table("ProyectosBeneficiarios")]
    public class ProyectoBeneficiario
    {
        [Column("Proyecto_Id")]
        public int ProyectoID { get; set; }
        public Proyecto Proyecto { get; set; }

        [Column("Beneficiario_Id")]
        public int BeneficiarioID { get; set; }
        public Beneficiario Beneficiario { get; set; }
    }
}
