using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyBills.Application.Common.Exceptions;
using MyBills.Application.Common.Interfaces;
using MyBills.Application.Shared.Accounts.Commands;
using MyBills.Domain.Entities;

namespace MyBills.Application.Accounts.Commands.UpdateAccount
{
    public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public UpdateAccountCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Unit> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var requestIds = new object[] {request.Id};
            var entity = await _applicationDbContext.Accounts.FindAsync(requestIds, cancellationToken);

            if (entity == null) throw new NotFoundException(nameof(Account), requestIds);

            entity.BankAccountNumber = request.BankAccountNumber;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}