
using ConstructorApp.Models;
using ConstructorApp.Repository;

namespace ConstructorApp.Services
{
    public class GenericService<T>(IGenericRepository<T> repository) : IGenericService<T> where T : BaseEntity
    {

        public async Task AddAsync(T entity)
        {
            await repository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await repository.GetByIdAsync(id);
            await repository.DeleteAsync(entity);
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task<(List<T> Items, int TotalPages)> GetPagedAsync(int page, int pageSize)
        {
        var allItems =await repository.GetAllAsync();
        var pagedData = allItems.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        var totalPages = (int)Math.Ceiling(allItems.Count() / (double)pageSize);

        return (pagedData, totalPages);
    }

        public Task UpdateAsync(T entity)
        {
            return repository.UpdateAsync(entity);
        }
    }
}