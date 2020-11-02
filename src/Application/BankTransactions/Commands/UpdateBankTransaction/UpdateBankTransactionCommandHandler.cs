using MediatR;
using MyBills.Application.Common.Exceptions;
using MyBills.Application.Common.Interfaces;
using MyBills.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyBills.Application.BankTransactions.Commands.UpdateBankTransaction
{
    public class UpdateBankTransactionCommandHandler : IRequestHandler<UpdateBankTransactionCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateBankTransactionCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task<Unit> Handle(UpdateBankTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.BankTransactions.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(BankTransaction), request.Id);
            }

            entity.Amount = request.Amount;
            entity.CategoryId = request.CategoryId;
            entity.Memo = request.Memo;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}