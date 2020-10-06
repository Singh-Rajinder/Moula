using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moula.Domain.Entities;

namespace Moula.Persistence.Configurations
{
    public class CustomerConfiguration: IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(i => i.Name).IsRequired().HasMaxLength(255);
            builder.Property(i => i.Balance).IsRequired();
        }
    }
}
