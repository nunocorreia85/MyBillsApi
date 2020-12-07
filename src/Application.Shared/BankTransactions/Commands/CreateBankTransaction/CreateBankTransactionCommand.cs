using MediatR;

namespace MyBills.Application.Shared.BankTransactions.Commands.CreateBankTransaction
{
    public class CreateBankTransactionCommand : IRequest<long>
    {
        public string Memo { get; set; }
        public decimal Amount { get; set; }
        public long CategoryId { get; set; }
        public long AccountId { get; set; }
    }
}