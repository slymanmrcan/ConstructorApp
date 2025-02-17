namespace ConstructorApp.Models
{
    public class ProjectAssignment : BaseEntity
    {
        public int ProjectId { get; set; }
        public int AppUserId { get; set; }
        public virtual Project? Project { get; set; }
        public virtual AppUser? AppUser { get; set; }
    }
}