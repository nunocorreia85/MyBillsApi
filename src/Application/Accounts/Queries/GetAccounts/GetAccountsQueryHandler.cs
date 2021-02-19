using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBills.Application.Common.Interfaces;
using MyBills.Application.Shared.Accounts.Queries;
using MyBills.Domain.Entities;

namespace MyBills.Application.Accounts.Queries.GetAccounts
{
    internal class GetAccountsQueryHandler : IRequestHandler<GetAccountQuery, IEnumerable<Account>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetAccountsQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Account>> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            var query = _applicationDbContext.Accounts.AsQueryable();
            if (request.UserId == Guid.Empty)
            {
                return await query.ToListAsync(cancellationToken);
            }

            return await query.Where(account => account.UserId == request.UserId).ToListAsync(cancellationToken);
        }
    }
}