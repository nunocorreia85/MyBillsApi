using System.Collections.Generic;
using MediatR;

namespace MyBills.Application.Shared.Accounts.Commands.CloseAccounts
{
    public class CloseAccountsCommand : IRequest
    {
        public List<long> Ids { get; set; }
    }
}