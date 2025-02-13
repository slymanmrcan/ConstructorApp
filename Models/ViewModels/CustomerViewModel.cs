using ConstructorApp.Models;

namespace Models.ViewModels
{
    public class CustomerViewModel
    {
        public PaginationModel PaginationModel { get; set; }
        public List<Customer> Customers { get; set; }
    }
}