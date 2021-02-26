using System;
using MediatR;

namespace MyBills.Application.Shared.Accounts.Commands
{
    public class UpdateAccountCommand : IRequest
    {
        public long Id { get; set; }
        public string BankAccountNumber { get; set; }
        public Guid UserId { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}