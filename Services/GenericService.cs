
using ConstructorApp.Models;
using ConstructorApp.Repository;

namespace ConstructorApp.Services
{
    public class GenericService<T>(IGenericRepository<T> repository) : IGenericService<T> where T : BaseEntity
    {

        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}