using MediatR;
using MyBills.Domain.Enums;

namespace MyBills.Application.Shared.TransactionCategories.Commands
{
    public class UpdateTransactionCategoryCommand : IRequest<long>
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public RecurringPeriod? RecurringPeriod { get; set; }
        public long Id { get; set; }
    }
}