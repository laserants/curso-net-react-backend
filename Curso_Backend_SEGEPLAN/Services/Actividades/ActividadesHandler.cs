using Curso_Backend_SEGEPLAN.DataContext;
using Curso_Backend_SEGEPLAN.Entities;
using Microsoft.EntityFrameworkCore;

namespace Curso_Backend_SEGEPLAN.Services.Actividades
{
    public class ActividadesHandler : IActividadesHandler
    {
        private readonly ApplicationDbContext _context;

        public ActividadesHandler(ApplicationDbContext context) => this._context = context;


        public async Task<Actividad[]> GetAsync()
        {
            var actividadesDb = this._context.Actividades.ToArray();

            return await Task.FromResult(actividadesDb);
        }

        public async Task<Actividad> GetByIdAsync(int actividadId)
        {
            var actividad = await this._context.Actividades.FirstOrDefaultAsync(x => x.ActividadID == actividadId);

            return actividad;
        }

        public async Task<int> CreateAsync(Actividad actividad)
        {
            if (actividad == null)
                return 0;

            this._context.Actividades.Add(actividad);
            var rowAffected = await this._context.SaveChangesAsync();

            return rowAffected;
        }

        public async Task UpdateAsync(Actividad actividad)
        {
            this._context.Entry(actividad).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int actividadId)
        {
            var actividadAEliminar = new Actividad { ActividadID = actividadId };

            this._context.Actividades.Remove(actividadAEliminar);
            var rowAffected = await this._context.SaveChangesAsync();

            return rowAffected > 0;
        }

        public async Task<bool> ExistRecordAsync(int actividadId)
        {
            bool existeRegistro = await this._context.Actividades.AnyAsync(x => x.ActividadID == actividadId);

            return existeRegistro;
        } 
    }
}
