using System.Collections.Generic;
using MediatR;

namespace MyBills.Application.Accounts.Commands.CloseAccount
{
    public class CloseAccountCommand : IRequest
    {
        public List<long> Ids { get; set; }
    }
}