using MediatR;

namespace MyBills.Application.Accounts.Commands.CloseAccount
{
    public class CloseAccountCommand : IRequest
    {
        public string Id { get; set; }
    }
}