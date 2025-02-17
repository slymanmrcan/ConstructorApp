using ConstructorApp.Models;

namespace ConstructorApp.Services
{
    public interface IGenericService<T> where T : BaseEntity
    {
        Task<IQueryable<T>> GetAllAsync();
        Task<(List<T> Items, int TotalPages)> GetPagedAsync(int page, int pageSize);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task DeleteAsync(int id);

    }
}