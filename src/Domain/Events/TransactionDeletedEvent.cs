using MyBills.Domain.Common;
using MyBills.Domain.Entities;

namespace MyBills.Domain.Events
{
    internal class TransactionDeletedEvent : DomainEvent
    {
        private readonly Transaction transaction;

        public TransactionDeletedEvent(Transaction transaction)
        {
            this.transaction = transaction;
        }
    }
}