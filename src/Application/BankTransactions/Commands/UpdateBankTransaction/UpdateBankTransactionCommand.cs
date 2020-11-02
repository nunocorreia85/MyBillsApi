using MediatR;

namespace MyBills.Application.BankTransactions.Commands.UpdateBankTransaction
{
    public class UpdateBankTransactionCommand : IRequest
    {
        public string Id { get; set; }
        public string Memo { get; set; }
        public decimal Amount { get; set; }
        public string CategoryId { get; set; }
    }
}