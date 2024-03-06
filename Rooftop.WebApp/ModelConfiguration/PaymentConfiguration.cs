using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rooftop.WebApp.Models;

namespace Rooftop.WebApp.ModelConfiguration;

public class PaymentConfiguration: IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable(nameof(Payment));
        builder.HasKey(x => x.Id); 
        builder.HasOne(x => x.User).WithMany(x => x.Payments).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
    }
}

