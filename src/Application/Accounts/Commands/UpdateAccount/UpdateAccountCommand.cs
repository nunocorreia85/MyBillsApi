using MediatR;

namespace MyBills.Application.Accounts.Commands.UpdateAccount
{
    public class UpdateAccountCommand : IRequest
    {
        public long Id { get; set; }
        public string OwnerName { get; set; }
        public string BankAccountNumber { get; set; }
    }
}