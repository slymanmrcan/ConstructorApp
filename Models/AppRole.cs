using Microsoft.AspNetCore.Identity;

namespace ConstructorApp.Models
{
    public class AppRole : IdentityRole<int>
    {
        public DateTime CreatedDate { get; set; }= DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}