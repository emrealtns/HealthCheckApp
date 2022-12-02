using HealthCheckApp.Data.Context;
using HealthCheckApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HealthCheckApp.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IEntityBase
    {
        private readonly HealthCheckContext _dbContext;
        public DbSet<T> Table { get; private set; }
        public Repository(HealthCheckContext dbContext)
        {
            _dbContext = dbContext;
            Table = _dbContext.Set<T>();
        }
        public async Task<T> GetByIdAsync(int id) => await Table.FirstOrDefaultAsync(x => x.Id.Equals(id));
        public IQueryable<T> Where(Expression<Func<T, bool>> expression) => Table
                .Where(expression).AsNoTracking();
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression) => await Table.AnyAsync(expression);
        public async Task<IEnumerable<T>> GetAllAsync() => await Table.ToListAsync();
        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = Table;
            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync().ConfigureAwait(false);
        }
        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
            await SaveChangesAsync();
        }
        public async Task Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var willDeleted = await Table.FindAsync(id)
                ?? throw new Exception($"{nameof(T)} not found!");
            Table.Remove(willDeleted);
            await SaveChangesAsync();
        }
        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();    
    }
}