using ConstructorApp.Services;

namespace ConstructorApp.Extentions
{
    public static class AddServices
    {
        public static IServiceCollection AddServicesExtention(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddScoped<ICustomerService, CustomerService>();
            return services;
        }
    }
}