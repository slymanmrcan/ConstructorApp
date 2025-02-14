using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConstructorApp.Models
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser,AppRole, int>(options)
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProjectAssignment> ProjectAssignments { get; set; }
        
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        entry.Entity.IsDeleted = false;
                        entry.Entity.ModifiedDate = null;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedDate = DateTime.UtcNow;
                        // CreatedAt'in değiştirilmemesini sağla
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
