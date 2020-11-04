using MediatR;

namespace MyBills.Application.BankTransactions.Commands.DeleteBankTransaction
{
    public class DeleteBankTransactionCommand : IRequest
    {
        public long Id { get; set; }
    }
}