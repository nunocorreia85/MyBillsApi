using MediatR;
using System;

namespace MyBills.Application.Shared.Accounts.Commands
{
    public class CreateAccountCommand : IRequest<long>
    {
        public Guid ExternalId { get; set; }
        public decimal Balance { get; set; }
        public string BankAccountNumber { get; set; }
    }
}