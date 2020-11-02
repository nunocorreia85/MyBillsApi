using MyBills.Domain.Common;
using MyBills.Domain.Entities;

namespace MyBills.Domain.Events
{
    public class BankTransactionDeletedEvent : DomainEvent
    {
        private readonly BankTransaction _bankTransaction;

        public BankTransactionDeletedEvent(BankTransaction bankTransaction)
        {
            _bankTransaction = bankTransaction;
        }
    }
}