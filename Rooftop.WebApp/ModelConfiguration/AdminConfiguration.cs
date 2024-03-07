using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rooftop.WebApp.Models;

namespace Rooftop.WebApp.ModelConfiguration;

public class AdminConfiguration : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.ToTable(nameof(Admin));
        builder.HasKey(x => x.Id);
    }
}