
using ConstructorApp.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace ConstructorApp.Repository
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await context.Database.BeginTransactionAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}