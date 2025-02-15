using ConstructorApp.Models;

namespace Models.ViewModels
{
    public class UserViewModel
    {
         public PaginationModel PaginationModel { get; set; }
        public List<AppUser> AppUser { get; set; }
    }
}