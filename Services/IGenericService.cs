using ConstructorApp.Models;

namespace ConstructorApp.Services
{
    public interface IGenericService<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}