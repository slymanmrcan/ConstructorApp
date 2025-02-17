using System.Threading.Tasks;
using ConstructorApp.Models;
using ConstructorApp.Repository;
using Microsoft.EntityFrameworkCore;
using Models.ChartViewModels;

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

        public async Task<List<Top3Project>> GetPrize()
        {
            var projects = await repository.GetAllAsync();
            var completed = projects.Where(p => p.Status == "Completed");

            var top3 = completed.Where(p => p.Prize > 0)
                                .OrderByDescending(p => p.Prize)
                                .Take(3)
                                .Select(p => new Top3Project { Prize = p.Prize, Name = p.Name })
                                .ToList();

            while (top3.Count < 3) // 3'ten az eleman varsa 0 ve "Bilinmeyen" ekleyelim
            {
                top3.Add(new Top3Project { Prize = 0, Name = "Bilinmeyen" });
            }

            return top3;
        }

        public async Task<List<AllProject>> GetAllProjects()
        {
            var projects = await repository.GetAllAsync();
            var list = projects.Select(p => new AllProject
            {
                Name = p.Name,
                Prize = p.Prize
            }).ToList();
            return list;
        }

        public async Task<double> GetAllPrize()
        {
            var projects = await repository.GetAllAsync();
            return projects.Select(p => p.Prize).Sum();
        }
    }
}