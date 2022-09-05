using Curso_Backend_SEGEPLAN.Entities;

namespace Curso_Backend_SEGEPLAN.Services.ProyetosBeneficiarios
{
    public interface IProyectosBeneficiariosHandler
    {
        Task<ProyectoBeneficiario[]> GetAsync();
        Task<ProyectoBeneficiario> GetByProjectIdAndBeneficiaryIdAsync(int projectId, int beneficiaryId);
        Task<int> CreateAsync(ProyectoBeneficiario projectBeneficiary);
    }
}
