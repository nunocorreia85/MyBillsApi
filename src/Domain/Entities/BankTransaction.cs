using MyBills.Domain.Common;
using MyBills.Domain.Events;
using System.Collections.Generic;

namespace MyBills.Domain.Entities
{
    public class BankTransaction : AuditableEntity, IHasDomainEvent
    {
        public string Memo { get; set; }
        
        public decimal Amount { get; set; }
        
        public long CategoryId { get; set; }
        
        public long AccountId { get; set; }
        
        private bool _deleted;

        public bool Deleted
        {
            get => _deleted;
            set
            {
                if (value == true && _deleted == false)
                {
                    DomainEvents.Add(new BankTransactionDeletedEvent(this));
                }
                _deleted = value;
            }
        }

        public TransactionCategory TransactionCategory { get; set; }

        public Account Account { get; set; }

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}