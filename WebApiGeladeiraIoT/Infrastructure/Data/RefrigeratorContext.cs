using ApiRefrigerator.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RefrigeratorContext : DbContext
    {
        public RefrigeratorContext(DbContextOptions<RefrigeratorContext> options) : base(options)
        {
        }

        public DbSet<Refrigerator> Refrigerator { get; set; }
    }
}
