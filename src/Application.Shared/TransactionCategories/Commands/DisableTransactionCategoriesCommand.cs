using System.Collections.Generic;
using MediatR;

namespace MyBills.Application.Shared.TransactionCategories.Commands
{
    public class DisableTransactionCategoriesCommand : IRequest
    {
        public List<long> Ids { get; set; }
    }
}