using System.Collections.Generic;
using MediatR;

namespace MyBills.Application.TransactionCategories.Commands.DisableTransactionCategories
{
    public class DisableTransactionCategoriesCommand : IRequest
    {
        public List<long> Ids { get; set; }
    }
}