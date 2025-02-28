using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyApplicationDomain.Entities;

namespace MyApplicationDataLayer.DataContext
{
    public class AppDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Request> Requests { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public override DbSet<Role> Roles { get; set; }
        public override DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Role>()
                .HasData(
                new Role { Id = 1, Name = "user", NormalizedName = "USER" },
                new Role { Id = 2, Name = "admin", NormalizedName = "ADMIN" });

            builder.Entity<IdentityUserLogin<int>>().HasNoKey();
            builder.Entity<IdentityUserRole<int>>().HasNoKey();
            builder.Entity<IdentityUserToken<int>>().HasNoKey();

            builder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .IsRequired();

            builder.Entity<User>()
                .HasMany(u => u.RefreshTokens)
                .WithOne(rt => rt.User);

        }
    }
}
