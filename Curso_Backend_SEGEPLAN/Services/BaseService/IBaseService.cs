using System.Linq.Expressions;

namespace Curso_Backend_SEGEPLAN.Services.BaseService
{
    public interface IBaseService<T> where T : class
    {
        Task<T[]> GetAsync(Expression<Func<T, bool>> whereCondition = null, string includeProperties = "");
        Task<T> GetByIdAsync(int id);
        Task<int> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistRecordAsync(Expression<Func<T, bool>> expression);
    }
}
