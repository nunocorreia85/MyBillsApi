using MediatR;

namespace MyBills.Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommand : IRequest<string>
    {
        public string OwnerName { get; set; }
        public decimal Balance { get; set; }
        public string BankAccountNumber { get; set; }
    }
}