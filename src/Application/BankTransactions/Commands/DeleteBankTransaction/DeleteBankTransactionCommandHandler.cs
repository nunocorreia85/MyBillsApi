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
        private readonly IApplicationDbContext _context;

        public DeleteBankTransactionCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<Unit> Handle(DeleteBankTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.BankTransactions.FindAsync(request.Id);

            if (entity == null) throw new NotFoundException(nameof(BankTransaction), request.Id);

            entity.Deleted = true;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}