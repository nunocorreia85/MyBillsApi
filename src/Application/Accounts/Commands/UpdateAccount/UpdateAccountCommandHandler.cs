using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var entity =
                await _applicationDbContext.Accounts.FirstOrDefaultAsync(c => c.UserId == request.UserId,
                    cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Account), request.UserId);
            }

            entity.BankAccountNumber = request.BankAccountNumber;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}