using System.Threading;
using System.Threading.Tasks;
using MediatR;
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
            var requestIds = new object[] {request.Id};
            var entity = await _applicationDbContext.Accounts.FindAsync(requestIds, cancellationToken);

            if (entity == null) throw new NotFoundException(nameof(Account), requestIds);

            entity.Closed = true;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}