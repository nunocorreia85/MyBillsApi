using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBills.Domain.Entities;

namespace MyBills.Infrastructure.Persistence.Configurations
{
    public class BankTransactionConfiguration : IEntityTypeConfiguration<BankTransaction>
    {
        public void Configure(EntityTypeBuilder<BankTransaction> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasPartitionKey(k => k.Id);

            builder.Ignore(e => e.DomainEvents);

            builder.Property(t => t.Memo)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
