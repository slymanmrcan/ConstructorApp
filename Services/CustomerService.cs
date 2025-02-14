using ConstructorApp.Models;
using ConstructorApp.Repository;

namespace ConstructorApp.Services
{
    public class CustomerService(IGenericRepository<Customer> repository) : GenericService<Customer>(repository), ICustomerService
    {
    }
}