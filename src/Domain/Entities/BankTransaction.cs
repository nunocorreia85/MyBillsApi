using System.Collections.Generic;
using MyBills.Domain.Common;
using MyBills.Domain.Events;

namespace MyBills.Domain.Entities
{
    public class BankTransaction : AuditableEntity, IHasDomainEvent
    {
        private bool _deleted;
        public string Memo { get; set; }

        public decimal Amount { get; set; }

        public long CategoryId { get; set; }

        public long AccountId { get; set; }

        public bool Deleted
        {
            get => _deleted;
            set
            {
                if (value && _deleted == false) DomainEvents.Add(new BankTransactionDeletedEvent(this));
                _deleted = value;
            }
        }

        public TransactionCategory TransactionCategory { get; set; }

        public Account Account { get; set; }

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}