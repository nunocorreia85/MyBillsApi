using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBills.Application.Common.Interfaces;
using MyBills.Application.Shared.BankTransactions.Queries;
using MyBills.Domain.Entities;

namespace MyBills.Application.BankTransactions.Queries
{
    public class GetBankTransactionsQueryHandler
        : IRequestHandler<GetBankTransactionsQuery, IEnumerable<BankTransaction>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetBankTransactionsQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BankTransaction>> Handle(GetBankTransactionsQuery request,
            CancellationToken cancellationToken)
        {
            var transactions = await _dbContext.BankTransactions
                .Where(transaction => request.Ids.Contains(transaction.Id)).ToListAsync(cancellationToken);
            return transactions;
        }
    }
}