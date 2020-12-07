using System.Collections.Generic;
using MediatR;

namespace MyBills.Application.Shared.BankTransactions.Commands.DeleteBankTransactions
{
    public class DeleteBankTransactionsCommand : IRequest
    {
        public List<long> Ids { get; set; }
    }
}