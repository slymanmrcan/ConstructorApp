using ConstructorApp.Extensions;
using ConstructorApp.Extentions;
using ConstructorApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSqlServer(builder.Configuration);
builder.Services.AddIdentitySettings();
builder.Services.AddRepositoryExtention().AddServicesExtention();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()  // Konsola yaz
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)  // Dosyaya yaz
    .WriteTo.MSSqlServer(
        connectionString: connectionString,
        tableName: "Logs",
        autoCreateSqlTable: true
    )
    .CreateLogger();
builder.Host.UseSerilog();

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
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
