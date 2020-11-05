using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyBills.Application.Common.Interfaces;
using MyBills.Domain.Entities;
using MyBills.Domain.Events;

namespace MyBills.Application.BankTransactions.Commands.CreateBankTransaction
{
    public class CreateBankTransactionCommandHandler : IRequestHandler<CreateBankTransactionCommand, long>
    {
        private readonly IApplicationDbContext _context;

        public CreateBankTransactionCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<long> Handle(CreateBankTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = new BankTransaction
            {
                AccountId = request.AccountId,
                Amount = request.Amount,
                CategoryId = request.CategoryId,
                Memo = request.Memo
            };

            entity.DomainEvents.Add(new BankTransactionCreatedEvent(entity));

            _context.BankTransactions.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}