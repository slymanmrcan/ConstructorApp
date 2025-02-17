using ConstructorApp.Models;
using Models.ChartViewModels;

namespace ConstructorApp.Services
{
    public interface IProjectService:IGenericService<Project>
    {
        Task<IQueryable<Project>> GetCountAsync();
        Task<int> GetCountFinished();
        Task<int> GetCountPending();
        Task<int> GetCountProgress();

        Task<List<Top3Project>> GetPrize();

        Task<List<AllProject>> GetAllProjects();

        Task<double> GetAllPrize();
    } 
}