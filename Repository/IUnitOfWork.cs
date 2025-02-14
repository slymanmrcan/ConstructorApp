namespace ConstructorApp.Repository
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}