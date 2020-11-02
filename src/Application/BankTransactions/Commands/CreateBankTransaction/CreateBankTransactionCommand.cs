using MediatR;

namespace MyBills.Application.BankTransactions.Commands.CreateBankTransaction
{
    public class CreateBankTransactionCommand : IRequest<string>
    {
        public string Memo { get; set; }
        public decimal Amount { get; set; }
        public string CategoryId { get; set; }
        public string AccountId { get; set; }
    }
}