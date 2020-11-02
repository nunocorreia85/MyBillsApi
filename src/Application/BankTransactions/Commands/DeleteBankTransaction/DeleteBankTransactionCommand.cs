using MediatR;

namespace MyBills.Application.BankTransactions.Commands.DeleteBankTransaction
{
    public class DeleteBankTransactionCommand : IRequest
    {
        public string Id { get; set; }
    }
}