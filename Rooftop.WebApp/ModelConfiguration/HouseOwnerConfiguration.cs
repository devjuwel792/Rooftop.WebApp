using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Rooftop.WebApp.Models;

namespace Rooftop.WebApp.ModelConfiguration;

public class HouseOwnerConfiguration : IEntityTypeConfiguration<HouseOwners>
{
    public void Configure(EntityTypeBuilder<HouseOwners> builder)
    {
        builder.ToTable(nameof(HouseOwners));
        builder.HasKey(x => x.Id);
    }
}