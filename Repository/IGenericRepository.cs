using System.Linq.Expressions;
using ConstructorApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstructorApp.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        DbSet<T> _dbSet { get; }
        Task<T?> GetByIdAsync(int id);
        Task<IQueryable<T>> GetAllAsync();
       IQueryable<T> FindAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveAsync();
    }
}