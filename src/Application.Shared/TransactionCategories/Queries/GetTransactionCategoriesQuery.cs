using System.Collections.Generic;
using MediatR;
using MyBills.Domain.Entities;

namespace MyBills.Application.TransactionCategories.Queries
{
    public class GetTransactionCategoriesQuery : IRequest<IEnumerable<TransactionCategory>>
    {
        public List<long> Ids { get; set; }
    }
}