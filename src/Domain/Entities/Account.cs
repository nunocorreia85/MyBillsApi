﻿using MyBills.Domain.Common;

namespace MyBills.Domain.Entities
{
    public class Account : AuditableEntity
    {
        public string OwnerName { get; set; }
        public decimal Balance { get; set; }
        public string BankAccountNumber { get; set; }
        public bool Closed { get; set; }
    }
}