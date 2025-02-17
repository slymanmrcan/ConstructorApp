using ConstructorApp.Models;
using ConstructorApp.Repository;
using Microsoft.EntityFrameworkCore;

namespace ConstructorApp.Services
{
    public class ProjectAssignmentService(IGenericRepository<ProjectAssignment> repository) :
    GenericService<ProjectAssignment>(repository), IProjectAssignmentService
    {
        public async Task<(List<ProjectAssignment> Items, int TotalPages)> GetPagedAsyncWithInclude(int page, int pageSize)
        {
            var allItems = await repository.GetAllAsync();

            var joinedData = allItems.Include(p => p.Project).Include(p => p.AppUser).ToList();

            var pagedData = joinedData.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var totalPages = (int)Math.Ceiling(joinedData.Count() / (double)pageSize);
            return (pagedData, totalPages);
        }
    }
}