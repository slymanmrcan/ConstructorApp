using ConstructorApp.Models;
using ConstructorApp.Repository;

namespace ConstructorApp.Services
{
    public class ProjectService(IGenericRepository<Project> repository) : GenericService<Project>(repository), IProjectService
    {
        public async Task<IQueryable<Project>> GetCountAsync()
        {
            var projects = await repository.GetAllAsync();
            return projects.Where(p => p.Status == "Tamamlandı").AsQueryable();
        }

        public async Task<int> GetCountFinished()
        {
            var projects = await repository.GetAllAsync();
            return projects.Where(p => p.Status == "Completed").Count();
        }

        public async Task<int> GetCountProgress()
        {
            var projects = await repository.GetAllAsync();
            return projects.Where(p => p.Status == "InProgress").Count();
        }

        public async Task<int> GetCountPending()
        {
            var projects = await repository.GetAllAsync();
            return projects.Where(p => p.Status == "Pending").Count();
        }
    }
}