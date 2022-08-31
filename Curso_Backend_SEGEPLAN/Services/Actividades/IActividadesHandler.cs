using Curso_Backend_SEGEPLAN.Entities;

namespace Curso_Backend_SEGEPLAN.Services.Actividades
{
    public interface IActividadesHandler
    {
        Task<Actividad[]> GetAsync();
        Task<Actividad> GetByIdAsync(int actividadId);
        Task<int> CreateAsync(Actividad actividad);
        Task UpdateAsync(Actividad actividad);
        Task<bool> DeleteAsync(int actividadId);
        Task<bool> ExistRecordAsync(int actividadId);
    }
}
