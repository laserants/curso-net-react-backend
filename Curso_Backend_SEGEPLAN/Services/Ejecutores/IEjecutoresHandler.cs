using Curso_Backend_SEGEPLAN.Entities;

namespace Curso_Backend_SEGEPLAN.Services.Ejecutores
{
    public interface IEjecutoresHandler
    {
        Task<Ejecutor[]> GetAsync();
        Task<Ejecutor> GetByIdAsync(int executorId);
        Task<int> CreateAsync(Ejecutor executor);
        Task UpdateAsync(Ejecutor executor);
        Task<bool> DeleteAsync(int executorId);
        Task<bool> ExistRecordAsync(int executorId);
    }
}
