using MediatR;
using System.Collections.Generic;

namespace MyBills.Application.Shared.Accounts.Commands
{
    public class CloseAccountsCommand : IRequest
    {
        public List<long> Ids { get; set; }
    }
}