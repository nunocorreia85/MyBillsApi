using MediatR;

namespace MyBills.Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommand : IRequest<long>
    {
        public string ExternalId { get; set; }
        public decimal Balance { get; set; }
        public string BankAccountNumber { get; set; }
    }
}