using ConstructorApp.Models;

namespace ConstructorApp.Services
{
    public interface IProjectAssignmentService: IGenericService<ProjectAssignment>
    {
        Task<(List<ProjectAssignment> Items, int TotalPages)> GetPagedAsyncWithInclude(int page, int pageSize);
    }
}