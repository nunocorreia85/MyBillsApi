using System;
using MediatR;

namespace MyBills.Application.Shared.Accounts.Commands
{
    public class CreateAccountCommand : IRequest<long>
    {
        public Guid UserId { get; set; }

        public decimal Balance { get; set; }

        public string BankAccountNumber { get; set; }

        public string Email { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}