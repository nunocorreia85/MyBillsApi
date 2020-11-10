using System.Collections.Generic;
using MediatR;
using MyBills.Domain.Entities;

namespace MyBills.Application.BankTransactions.Queries
{
    public class GetBankTransactionsQuery : IRequest<IEnumerable<BankTransaction>>
    {
        public List<long> Ids { get; set; }
    }
}