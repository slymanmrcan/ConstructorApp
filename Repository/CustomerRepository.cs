using ConstructorApp.Models;

namespace ConstructorApp.Repository
{
    public class CustomerRepository(AppDbContext context) : GenericRepository<Customer>(context), ICustomerRepository
    {
        
    }
}