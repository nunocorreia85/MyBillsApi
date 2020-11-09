using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBills.Application.Common.Exceptions;
using MyBills.Application.Common.Interfaces;
using MyBills.Domain.Entities;

namespace MyBills.Application.Accounts.Commands.CloseAccount
{
    public class CloseAccountCommandHandler : IRequestHandler<CloseAccountCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public CloseAccountCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Unit> Handle(CloseAccountCommand request, CancellationToken cancellationToken)
        {
            var requestIds = new object[] {request.Ids};
            var entities = await _applicationDbContext.Accounts.Where(account => request.Ids.Contains(account.Id)).ToListAsync(cancellationToken);

            if (entities == null) throw new NotFoundException(nameof(Account), requestIds);

            entities.ForEach(account => account.Closed = true);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}