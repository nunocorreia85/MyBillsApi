using Microsoft.EntityFrameworkCore;
using MyBills.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyBills.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<BankTransaction> BankTransactions { get; set; }

        DbSet<Account> Accounts { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}