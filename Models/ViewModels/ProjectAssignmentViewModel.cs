using ConstructorApp.Models;

namespace Models.ViewModels
{
    public class ProjectAssignmentViewModel
    {
        public PaginationModel PaginationModel { get; set; }
        public List<ProjectAssignment> ProjectAssignment { get; set; }
        public List<Project> Projects { get; set; }
        public List<AppUser> AppUsers { get; set; }
    }
    
}