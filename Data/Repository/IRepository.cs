using HealthCheckApp.Data.Entities;
using System.Linq.Expressions;

namespace HealthCheckApp.Data.Repository
{
    public interface IRepository<T> where T : class, IEntityBase
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task Update(T entity);
        Task Delete(int id);
        Task SaveChangesAsync();
    }
}
