using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBills.Application.Common.Exceptions;
using MyBills.Application.Common.Interfaces;
using MyBills.Application.Shared.BankTransactions.Commands.DeleteBankTransactions;
using MyBills.Domain.Entities;

namespace MyBills.Application.BankTransactions.Commands.DeleteBankTransactions
{
    public class DeleteBankTransactionsCommandHandler : IRequestHandler<DeleteBankTransactionsCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DeleteBankTransactionsCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Unit> Handle(DeleteBankTransactionsCommand request, CancellationToken cancellationToken)
        {
            var transactions = await _applicationDbContext.BankTransactions
                .Where(transaction => request.Ids.Contains(transaction.Id))
                .ToListAsync(cancellationToken);

            if (transactions == null)
                throw new NotFoundException(nameof(BankTransaction), string.Join(",", request.Ids));

            transactions.ForEach(transaction => transaction.Deleted = true);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}