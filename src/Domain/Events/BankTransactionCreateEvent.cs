using MyBills.Domain.Common;
using MyBills.Domain.Entities;

namespace MyBills.Domain.Events
{
    public class BankTransactionCreatedEvent : DomainEvent
    {
        private readonly BankTransaction _bankTransaction;

        public BankTransactionCreatedEvent(BankTransaction bankTransaction)
        {
            _bankTransaction = bankTransaction;
        }
    }
}