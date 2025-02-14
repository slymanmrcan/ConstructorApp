namespace ConstructorApp.Models
{
    public class ProjectAssignment : BaseEntity
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public Project Project { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}