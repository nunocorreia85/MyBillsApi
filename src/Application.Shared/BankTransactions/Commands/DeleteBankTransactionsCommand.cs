using MediatR;
using System.Collections.Generic;

namespace MyBills.Application.Shared.BankTransactions.Commands
{
    public class DeleteBankTransactionsCommand : IRequest
    {
        public List<long> Ids { get; set; }
    }
}