using Microsoft.EntityFrameworkCore.Storage;

namespace ConstructorApp.Repository
{
    public interface IUnitOfWork
    { 
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<int> SaveChangesAsync();
    }
}