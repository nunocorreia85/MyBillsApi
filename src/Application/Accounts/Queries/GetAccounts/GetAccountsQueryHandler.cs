using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBills.Application.Common.Interfaces;
using MyBills.Application.Shared.Accounts.Queries;
using MyBills.Domain.Entities;

namespace MyBills.Application.Accounts.Queries.GetAccounts
{
    internal class GetAccountsQueryHandler : IRequestHandler<GetAccountQuery, Account>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetAccountsQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Account> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Accounts.FirstOrDefaultAsync(
                act => act.UserId == request.UserId,
                cancellationToken);
        }
    }
}