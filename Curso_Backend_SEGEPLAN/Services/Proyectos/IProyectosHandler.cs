using Curso_Backend_SEGEPLAN.Entities;

namespace Curso_Backend_SEGEPLAN.Services.Proyectos
{
    public interface IProyectosHandler
    {
        Task<Proyecto[]> GetAsync();
        Task<Proyecto> GetByIdAsync(int proyectoId);
        Task<int> CreateAsync(Proyecto proyecto);
        Task UpdateAsync(Proyecto proyecto);
        Task<bool> DeleteAsync(int proyectoId);
        Task<bool> ExistRecordAsync(int proyectoId);
    }
}
