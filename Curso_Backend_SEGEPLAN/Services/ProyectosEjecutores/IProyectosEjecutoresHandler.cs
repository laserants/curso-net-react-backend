using Curso_Backend_SEGEPLAN.Entities;

namespace Curso_Backend_SEGEPLAN.Services.ProyectosEjecutores
{
    public interface IProyectosEjecutoresHandler 
    {
        Task<ProyectoEjecutor[]> GetAsync();
        Task<ProyectoEjecutor> GetByProjectIdAndExecutorIdAsync(int projectId, int executorId);
        Task<int> CreateAsync(ProyectoEjecutor projectExecutor);
    }
}
