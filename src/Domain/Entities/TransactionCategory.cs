using MyBills.Domain.Common;
using MyBills.Domain.Enums;

namespace MyBills.Domain.Entities
{
    public class TransactionCategory : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public RecurringPeriod? RecurringPeriod { get; set; }
    }
}
