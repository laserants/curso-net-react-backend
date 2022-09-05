using Curso_Backend_SEGEPLAN.DataContext;
using Curso_Backend_SEGEPLAN.Entities;
using Microsoft.EntityFrameworkCore;

namespace Curso_Backend_SEGEPLAN.Services.ProyectosEjecutores
{
    public class ProyectosEjecutoresHandler : IProyectosEjecutoresHandler
    {
        private readonly ApplicationDbContext _context;

        public ProyectosEjecutoresHandler(ApplicationDbContext applicationDbContext)  => this._context = applicationDbContext;

        public async Task<ProyectoEjecutor[]> GetAsync()
        {
            var proyectosEjecutoresDB = this._context.ProyectosEjecutores
                                                     .Include(x => x.Proyecto)
                                                        .ThenInclude(y => y.Actividades)
                                                     .Include(x => x.Ejecutor)
                                                     .ToArray();

            return await Task.FromResult(proyectosEjecutoresDB);
        }

        public async Task<ProyectoEjecutor> GetByProjectIdAndExecutorIdAsync(int projectId, int executorId)
        {
            var proyectoEjecutor = await this._context.ProyectosEjecutores
                                                      .Include(x => x.Proyecto)
                                                        .ThenInclude(y => y.Actividades)
                                                      .Include(x => x.Ejecutor)
                                                      .FirstOrDefaultAsync(x => x.ProyectoID == projectId && x.EjecutorID == executorId);

            return proyectoEjecutor;
        }

        public async Task<int> CreateAsync(ProyectoEjecutor projectExecutor)
        {
            if (projectExecutor == null)
                return 0;

            this._context.ProyectosEjecutores.Add(projectExecutor);
            var filasAfectadas = await this._context.SaveChangesAsync();

            return filasAfectadas;
        }
    }
}
