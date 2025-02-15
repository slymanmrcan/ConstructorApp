namespace ConstructorApp.Models
{
    public class Project:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Prize { get; set; }  
        public string Location { get; set; }
    }
}