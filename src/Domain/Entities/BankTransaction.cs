using MyBills.Domain.Common;
using MyBills.Domain.Events;
using System.Collections.Generic;

namespace MyBills.Domain.Entities
{
    public class BankTransaction : AuditableEntity, IHasDomainEvent
    {
        public string Memo { get; set; }
        public decimal Amount { get; set; }
        public string CategoryId { get; set; }
        public string AccountId { get; set; }
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

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
