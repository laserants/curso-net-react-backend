using Curso_Backend_SEGEPLAN.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Curso_Backend_SEGEPLAN.Services.BaseService
{
    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public BaseService(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<T[]> GetAsync(Expression<Func<T, bool>> whereCondition = null, string includeProperties = "")
        {
            IQueryable<T> query = this._context.Set<T>();

            if(whereCondition != null)
                query = query.Where(whereCondition);

            foreach (var includeProperty in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return await query.ToArrayAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await this._context.Set<T>().FindAsync(id);

            return entity;
        }

        public async Task<int> CreateAsync(T entity)
        {
            if (entity == null)
                return 0;

            this._context.Set<T>().Add(entity);
            var rowAffected = await this._context.SaveChangesAsync();

            return rowAffected;
        }

        public async Task UpdateAsync(T entity)
        {
            this._context.Entry(entity).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entityToDelete = await this.GetByIdAsync(id);

            this._context.Set<T>().Remove(entityToDelete);
            var rowAffected = await this._context.SaveChangesAsync();

            return rowAffected > 0;
        }

        public async Task<bool> ExistRecordAsync(Expression<Func<T, bool>> expression)
        {
            var existRecord = await this._context.Set<T>().AnyAsync(expression);

            return existRecord;
        }        
    }
}

