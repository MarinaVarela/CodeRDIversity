using ApiRefrigerator.Models;
using Infrastructure.Context.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class RefrigeratorContext : IdentityDbContext<ApplicationUser>
    {
        public RefrigeratorContext(DbContextOptions<RefrigeratorContext> options) : base(options)
        {
        }

        public DbSet<Refrigerator> Refrigerator { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=localhost;Database=db_refrigerator;Trusted_Connection=True;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connectionString)
                          .EnableSensitiveDataLogging();
        }
    }
}
