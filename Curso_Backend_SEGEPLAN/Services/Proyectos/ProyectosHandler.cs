using Curso_Backend_SEGEPLAN.DataContext;
using Curso_Backend_SEGEPLAN.Entities;
using Microsoft.EntityFrameworkCore;

namespace Curso_Backend_SEGEPLAN.Services.Proyectos
{
    public class ProyectosHandler : IProyectosHandler
    {
        private readonly ApplicationDbContext _context;

        public ProyectosHandler(ApplicationDbContext context) => this._context = context;        

        public async Task<Proyecto[]> GetAsync()
        {
            var proyectosDB = this._context.Proyectos.ToArray();

            return await Task.FromResult(proyectosDB);
        }

        public async Task<Proyecto> GetByIdAsync(int proyectoId)
        {
            var proyecto = await this._context.Proyectos.FirstOrDefaultAsync(x => x.ProyectoID == proyectoId);

            return await Task.FromResult(proyecto);
        }

        public async Task<int> CreateAsync(Proyecto proyecto)
        {
            if (proyecto == null)
                return 0;
            
            this._context.Proyectos.Add(proyecto);
            var rowAffected = await this._context.SaveChangesAsync();

            return rowAffected;
        }

        public async Task UpdateAsync(Proyecto proyecto)
        {
            this._context.Entry(proyecto).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int proyectoId)
        {
            var existeRegistro = await this.ExistRecordAsync(proyectoId);

            if(!existeRegistro)
                return false;

            var proyectoEliminar = new Proyecto { ProyectoID = proyectoId };

            this._context.Proyectos.Remove(proyectoEliminar);
            await this._context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistRecordAsync(int proyectoId)
        {
            if (proyectoId <= 0)
                throw new ArgumentException("Id del proyecto invalido");

            bool existeRegistro = await this._context.Proyectos.AnyAsync(x => x.ProyectoID == proyectoId);

            return existeRegistro;
        }
    }
}
