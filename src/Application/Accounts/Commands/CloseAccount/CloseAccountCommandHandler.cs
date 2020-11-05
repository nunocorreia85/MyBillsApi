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
            var entity = await _applicationDbContext.Accounts.FindAsync(request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(nameof(Account), request.Id);

            entity.Closed = true;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}