using MyBills.Domain.Common;
using MyBills.Domain.Entities;

namespace MyBills.Domain.Events
{
    public class BankTransactionEventBase : DomainEvent
    {
        private readonly BankTransaction _bankTransaction;

        public BankTransactionEventBase(BankTransaction bankTransaction)
        {
            _bankTransaction = bankTransaction;
        }

        public decimal Amount => _bankTransaction.Amount;
        public decimal AccountId => _bankTransaction.AccountId;
    }

    public class BankTransactionCreatedEvent : BankTransactionEventBase
    {
        public BankTransactionCreatedEvent(BankTransaction bankTransaction) : base(bankTransaction)
        {
        }
    }
}