using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Rooftop.WebApp.Models;

namespace Rooftop.WebApp.ModelConfiguration;

public class FarmConfiguration : IEntityTypeConfiguration<farm>
{
    public void Configure(EntityTypeBuilder<farm> builder)
    {
        builder.ToTable(nameof(farm));
        builder.HasKey(x => x.Id);
    }
}