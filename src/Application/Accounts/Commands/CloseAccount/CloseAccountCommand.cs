using MediatR;

namespace MyBills.Application.Accounts.Commands.CloseAccount
{
    public class CloseAccountCommand : IRequest
    {
        public long Id { get; set; }
    }
}