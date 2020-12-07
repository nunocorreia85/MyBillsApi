using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyBills.Application.Common.Exceptions;
using MyBills.Application.Common.Interfaces;
using MyBills.Application.Shared.BankTransactions.Commands;
using MyBills.Domain.Entities;

namespace MyBills.Application.BankTransactions.Commands.UpdateBankTransaction
{
    public class UpdateBankTransactionCommandHandler : IRequestHandler<UpdateBankTransactionCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public UpdateBankTransactionCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Unit> Handle(UpdateBankTransactionCommand request, CancellationToken cancellationToken)
        {
            var requestIds = new object[] {request.Id};
            var entity = await _applicationDbContext.BankTransactions.FindAsync(requestIds, cancellationToken);

            if (entity == null) throw new NotFoundException(nameof(BankTransaction), requestIds);

            entity.Amount = request.Amount;
            entity.CategoryId = request.CategoryId;
            entity.Memo = request.Memo;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}