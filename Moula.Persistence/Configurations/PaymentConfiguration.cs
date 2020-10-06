using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moula.Domain.Entities;

namespace Moula.Persistence.Configurations
{
    public class PaymentConfiguration: IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(i => i.Status).IsRequired().HasConversion<string>();
            builder.Property(i => i.Comment).HasMaxLength(500);
        }
    }
}
