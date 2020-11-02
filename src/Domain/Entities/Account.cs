﻿using MyBills.Domain.Common;

namespace MyBills.Domain.Entities
{
    public class Account : AuditableEntity
    {
        public string Owner { get; set; }
        public decimal Balance { get; set; }
        public string Iban { get; set; }
    }
}
