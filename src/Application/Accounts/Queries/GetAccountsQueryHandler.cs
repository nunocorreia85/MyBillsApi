using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBills.Application.Common.Interfaces;
using MyBills.Domain.Entities;

namespace MyBills.Application.Accounts.Queries
{
    internal class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, List<Account>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetAccountsQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<Account>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            var query = _applicationDbContext.Accounts.AsQueryable();
            if (request.Ids.Any()) query = query.Where(account => request.Ids.Contains(account.Id) );

            return await query.ToListAsync(cancellationToken);
        }
    }
}