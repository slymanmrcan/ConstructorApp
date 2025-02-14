using ConstructorApp.Extensions;
using ConstructorApp.Extentions;
using ConstructorApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentitySettings();
builder.Services.AddRepositoryExtention().AddServicesExtention();
builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Login/Login";
                options.LogoutPath = "/Login/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.Cookie.Name = "AuthCookie";
            });
builder.Services.AddControllersWithViews();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
