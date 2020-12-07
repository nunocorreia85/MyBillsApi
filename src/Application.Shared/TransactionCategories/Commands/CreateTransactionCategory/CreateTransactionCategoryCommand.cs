using MediatR;
using MyBills.Domain.Enums;

namespace MyBills.Application.Shared.TransactionCategories.Commands.CreateTransactionCategory
{
    public class CreateTransactionCategoryCommand : IRequest<long>
    {
        public long AccountId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public RecurringPeriod? RecurringPeriod { get; set; }
    }
}