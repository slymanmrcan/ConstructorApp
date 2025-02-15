using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ConstructorApp.Models
{
    public class AppUser : IdentityUser<int>
    {
        [DisplayName("Ad")]
        public string FirstName { get; set; }
        [DisplayName("Soyad")]

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public DateTime CreatedDate { get; set; }= DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}