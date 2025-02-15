using ConstructorApp.Models;

namespace Models.ViewModels
{
    public class ProjectViewModel
    {
        public PaginationModel PaginationModel { get; set; }
        public List<Project> Project { get; set; }
    }
}