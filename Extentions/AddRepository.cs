using ConstructorApp.Repository;

namespace ConstructorApp.Extentions
{
    public static class AddRepository
    {
        public static IServiceCollection AddRepositoryExtention(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}