using MediatR;
using MyBills.Domain.Entities;
using System.Collections.Generic;

namespace MyBills.Application.Shared.TransactionCategories.Queries
{
    public class GetTransactionCategoriesQuery : IRequest<IEnumerable<TransactionCategory>>
    {
        public List<long> Ids { get; set; }
    }
}