using MyBills.Domain.Entities;

namespace MyBills.Domain.Events
{
    public class BankTransactionDeletedEvent : BankTransactionEventBase
    {
        public BankTransactionDeletedEvent(BankTransaction bankTransaction) : base(bankTransaction)
        {
        }
    }
}