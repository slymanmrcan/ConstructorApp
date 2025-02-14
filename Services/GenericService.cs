
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

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await repository.GetByIdAsync(id);
        }

        public Task UpdateAsync(T entity)
        {
            return repository.UpdateAsync(entity);
        }
    }
}