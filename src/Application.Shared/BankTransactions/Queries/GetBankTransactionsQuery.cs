using MediatR;
using MyBills.Domain.Entities;
using System.Collections.Generic;

namespace MyBills.Application.Shared.BankTransactions.Queries
{
    public class GetBankTransactionsQuery : IRequest<IEnumerable<BankTransaction>>
    {
        public List<long> Ids { get; set; }
    }
}