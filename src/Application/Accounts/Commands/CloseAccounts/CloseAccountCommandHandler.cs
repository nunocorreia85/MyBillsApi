using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBills.Application.Common.Exceptions;
using MyBills.Application.Common.Interfaces;
using MyBills.Application.Shared.Accounts.Commands;
using MyBills.Domain.Entities;

namespace MyBills.Application.Accounts.Commands.CloseAccounts
{
    public class CloseAccountCommandHandler : IRequestHandler<CloseAccountsCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public CloseAccountCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Unit> Handle(CloseAccountsCommand request, CancellationToken cancellationToken)
        {
            var entity =
                await _applicationDbContext.Accounts.FirstOrDefaultAsync(account => account.UserId == request.UserId,
                    cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Account), request.UserId);
            }

            entity.Closed = true;
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}