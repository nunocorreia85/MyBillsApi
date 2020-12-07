using MediatR;

namespace MyBills.Application.Shared.BankTransactions.Commands
{
    public class UpdateBankTransactionCommand : IRequest
    {
        public long Id { get; set; }
        public string Memo { get; set; }
        public decimal Amount { get; set; }
        public long CategoryId { get; set; }
    }
}