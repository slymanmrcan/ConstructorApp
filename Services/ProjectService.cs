using ConstructorApp.Models;
using ConstructorApp.Repository;

namespace ConstructorApp.Services
{
    public class ProjectService(IGenericRepository<Project> repository) : GenericService<Project>(repository), IProjectService
    {
    }
}