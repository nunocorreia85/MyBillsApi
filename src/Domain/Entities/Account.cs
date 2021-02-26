using System;
using MyBills.Domain.Common;

namespace MyBills.Domain.Entities
{
    public class Account : AuditableEntity
    {
        public Guid UserId { get; set; }
        public decimal Balance { get; set; }
        public string BankAccountNumber { get; set; }
        public bool Closed { get; set; }
        public string Email { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }
}