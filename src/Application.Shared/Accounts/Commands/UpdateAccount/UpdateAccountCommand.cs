using MediatR;

namespace MyBills.Application.Shared.Accounts.Commands.UpdateAccount
{
    public class UpdateAccountCommand : IRequest
    {
        public long Id { get; set; }
        public string BankAccountNumber { get; set; }
    }
}