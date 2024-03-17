using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rooftop.WebApp.Models;

namespace Rooftop.WebApp.ModelConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
       builder.ToTable(nameof(User));
        builder.HasKey(x => x.Id); builder.HasData(new
        {
            Id = 1,
            Email = "user@gmail.com",
            Password = "12345",
            UserName = "User",

        });
    }
}
