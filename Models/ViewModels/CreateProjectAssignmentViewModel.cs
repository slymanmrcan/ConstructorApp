using ConstructorApp.Models;

namespace Models.ViewModels
{
    public class CreateProjectAssignmentViewModel
    {
        public ProjectAssignment ProjectAssignment { get; set; }
        public IQueryable<Project> Projects { get; set; }
        public List<AppUser> AppUsers { get; set; }
    }
}