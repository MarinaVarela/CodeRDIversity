using ApiGeladeira.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GeladeiraContext : DbContext
    {
        public GeladeiraContext(DbContextOptions<GeladeiraContext> options) : base(options)
        {
        }

        public DbSet<ItemGeladeira> ItensGeladeira { get; set; }
    }
}
