using System.Linq.Expressions;
using ConstructorApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstructorApp.Repository
{
    public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : BaseEntity
    {
        public DbSet<T> _dbSet => context.Set<T>();

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

        public IQueryable<T> FindAsync(Expression<Func<T, bool>> expression)
        {
           return _dbSet.Where(expression).AsNoTracking();
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            return _dbSet.AsQueryable().AsNoTracking();
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}