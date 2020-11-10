using MediatR;
using MyBills.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBills.Application.TransactionCategories.Commands.CreateTransactionCategory
{
    public class CreateTransactionCategoryCommand : IRequest<long>
    {
        public long AccountId { get; internal set; }
        public string Description { get; internal set; }
        public string Name { get; internal set; }
        public RecurringPeriod? RecurringPeriod { get; internal set; }
    }
}