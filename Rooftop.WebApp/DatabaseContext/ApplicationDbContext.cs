using Microsoft.EntityFrameworkCore;
using Rooftop.WebApp.Models;
using Rooftop.WebApp.ViewModel;

namespace Rooftop.WebApp.DatabaseContext;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }


}
