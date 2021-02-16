using MediatR;
using System.Collections.Generic;

namespace MyBills.Application.Shared.TransactionCategories.Commands
{
    public class DisableTransactionCategoriesCommand : IRequest
    {
        public List<long> Ids { get; set; }
    }
}