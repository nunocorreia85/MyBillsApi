using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBills.Domain.Entities;

namespace MyBills.Infrastructure.Persistence.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable(nameof(Account));
            builder.Property(t => t.UserId)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(t => t.BankAccountNumber)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(t => t.Balance)
                .HasColumnType("decimal(18,2)");
        }
    }
}