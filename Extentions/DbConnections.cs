using ConstructorApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating.CodeCompilation;
using Serilog;

namespace ConstructorApp.Extensions
{
    public static class DbConnectionExtension
    {


        public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
        public static IServiceCollection AddSeriLog(this IServiceCollection services, IConfiguration configuration)
        {

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()  // Konsola yaz
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)  // Dosyaya yaz
                .WriteTo.MSSqlServer(
                    connectionString: configuration.GetConnectionString("DefaultConnection"),
                    tableName: "Logs",
                    autoCreateSqlTable: true
                )
                .CreateLogger();
            return services;
        }

        public static IServiceCollection AddIdentitySettings(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                // Şifre kuralları
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 2;

                // Kullanıcı kuralları
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;  // Email onayı istemiyorsan false yap
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }
        public static IServiceCollection ConfigureIdentityCookie(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie ayarları
                options.Cookie.Name = "AuthCookie"; // İstediğin isim
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Error/AccessDenied";
                options.SlidingExpiration = true;
                // İhtiyacın olursa diğer ayarları da yapabilirsin.
            });
            return services;

        }

        // public static IServiceCollection AddCookies(this IServiceCollection services)
        // {
        //     services.AddAuthentication(options =>
        //     {
        //         options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //         options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //     }).AddCookie(options =>
        //         {
        //             options.Cookie.Name = "AuthCookie";
        //             options.Cookie.HttpOnly = true;
        //             options.LoginPath = "/Account/Login";
        //             options.LogoutPath = "/Account/Logout";
        //             options.AccessDeniedPath = "/Error/AccessDenied";
        //             options.SlidingExpiration = true;
        //         });
        //     return services;
        // }
    }
}