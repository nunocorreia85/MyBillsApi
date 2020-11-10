using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBills.Application.Common.Interfaces;
using MyBills.Domain.Entities;

namespace MyBills.Application.TransactionCategories.Queries
{
    public class GetTransactionCategoriesQueryHandler
        : IRequestHandler<GetTransactionCategoriesQuery, IEnumerable<TransactionCategory>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetTransactionCategoriesQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TransactionCategory>> Handle(GetTransactionCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            var categories = await _dbContext.TransactionCategories
                .Where(cateogry => request.Ids.Contains(cateogry.Id)).ToListAsync(cancellationToken);
            return categories;
        }
    }
}