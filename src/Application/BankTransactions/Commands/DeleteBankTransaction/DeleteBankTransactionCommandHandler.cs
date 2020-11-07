using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyBills.Application.Common.Exceptions;
using MyBills.Application.Common.Interfaces;
using MyBills.Domain.Entities;

namespace MyBills.Application.BankTransactions.Commands.DeleteBankTransaction
{
    public class DeleteBankTransactionCommandHandler : IRequestHandler<DeleteBankTransactionCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DeleteBankTransactionCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Unit> Handle(DeleteBankTransactionCommand request, CancellationToken cancellationToken)
        {
            var requestIds = new object[] {request.Id};
            var entity = await _applicationDbContext.BankTransactions.FindAsync(requestIds, cancellationToken);

            if (entity == null) throw new NotFoundException(nameof(BankTransaction), requestIds);

            entity.Deleted = true;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}