using MediatR;
using MyBills.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBills.Application.TransactionCategories.Commands.UpdateTransactionCategory
{
    public class UpdateTransactionCategoryCommand : IRequest<long>
    {
        public string Description { get; internal set; }
        public string Name { get; internal set; }
        public RecurringPeriod? RecurringPeriod { get; internal set; }
        public long Id { get; internal set; }
    }
}