using MyBills.Domain.Common;
using MyBills.Domain.Events;
using System.Collections.Generic;

namespace MyBills.Domain.Entities
{
    public class Transaction : AuditableEntity, IHasDomainEvent
    {
        public int Id { get; set; }
        public string Memo { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public int AccountId { get; set; }
        private bool _deleted;
        public bool Deleted
        {
            get => _deleted;
            set
            {
                if (value == true && _deleted == false)
                {                    
                    DomainEvents.Add(new TransactionDeletedEvent(this));                    
                }
                _deleted = value;
            }
        }

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
