
using ConstructorApp.Models;

namespace ConstructorApp.Repository
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public async Task<int> SavaChagensAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}