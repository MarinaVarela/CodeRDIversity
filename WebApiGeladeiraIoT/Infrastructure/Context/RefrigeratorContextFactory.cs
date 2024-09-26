using Infrastructure.Context;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

public class RefrigeratorContextFactory : IDesignTimeDbContextFactory<RefrigeratorContext>
{
    public RefrigeratorContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RefrigeratorContext>();
        optionsBuilder.UseSqlServer("DefaultConnection");
        return new RefrigeratorContext(optionsBuilder.Options);
    }
}
