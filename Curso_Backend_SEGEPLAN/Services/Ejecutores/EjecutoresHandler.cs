using Curso_Backend_SEGEPLAN.DataContext;
using Curso_Backend_SEGEPLAN.Entities;
using Microsoft.EntityFrameworkCore;

namespace Curso_Backend_SEGEPLAN.Services.Ejecutores
{
    public class EjecutoresHandler : IEjecutoresHandler
    {
        private readonly ApplicationDbContext _context;

        public EjecutoresHandler(ApplicationDbContext context) => this._context = context;

        public async Task<Ejecutor[]> GetAsync()
        {
            var executorsDb = this._context.Ejecutores.ToArray();

            return await Task.FromResult(executorsDb);
        }

        public async Task<Ejecutor> GetByIdAsync(int executorId)
        {
            var executor = await this._context.Ejecutores.FirstOrDefaultAsync(x => x.EjecutorID == executorId);

            return await Task.FromResult(executor);
        }

        public async Task<int> CreateAsync(Ejecutor executor)
        {
            if (executor == null)
                return 0;

            this._context.Ejecutores.Add(executor);
            var rowAffected = await this._context.SaveChangesAsync();

            return rowAffected;
        }

        public async Task UpdateAsync(Ejecutor executor)
        {
            this._context.Entry(executor).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int executorId)
        {
            var executorToDelete = new Ejecutor() { EjecutorID = executorId };

            this._context.Ejecutores.Remove(executorToDelete);
            var rowAffected = await this._context.SaveChangesAsync();

            return rowAffected > 0;
        }

        public async Task<bool> ExistRecordAsync(int executorId)
        {
            bool existRecord = await this._context.Ejecutores.AnyAsync(x => x.EjecutorID == executorId);

            return existRecord;
        }
    }
}
