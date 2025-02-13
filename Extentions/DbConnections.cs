using ConstructorApp.Models;
using Microsoft.AspNetCore.Identity;

namespace ConstructorApp.Extensions
{
    public static class DbConnectionExtension
    {
        // public static string GetDbConnection(this IConfiguration configuration)
        // {
        //     var connectionString = configuration.GetConnectionString("DefaultConnection");

        //     if (string.IsNullOrEmpty(connectionString))
        //     {
        //         throw new Exception("Bağlantı dizesi bulunamadı! Lütfen appsettings.json dosyanızı kontrol edin.");
        //     }

        //     return connectionString;
        // }
    
    public static IServiceCollection AddIdentitySettings(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                // Şifre kuralları
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;

                // Kullanıcı kuralları
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;  // Email onayı istemiyorsan false yap
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}