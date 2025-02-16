using ConstructorApp.Models;

namespace ConstructorApp.Services
{
    public interface IProjectService:IGenericService<Project>
    {
        Task<IQueryable<Project>> GetCountAsync();
        Task<int> GetCountFinished();
        Task<int> GetCountPending();
        Task<int> GetCountProgress();
    }
}