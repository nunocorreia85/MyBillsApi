using System;
using MediatR;

namespace MyBills.Application.Shared.Accounts.Commands
{
    public class CloseAccountsCommand : IRequest
    {
        public Guid UserId { get; set; }
    }
}