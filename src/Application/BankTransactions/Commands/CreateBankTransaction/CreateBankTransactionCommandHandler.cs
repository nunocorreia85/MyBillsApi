using MediatR;
using MyBills.Application.Common.Interfaces;
using MyBills.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace MyBills.Application.BankTransactions.Commands.CreateBankTransaction
{
    public class CreateBankTransactionCommandHandler : IRequestHandler<CreateBankTransactionCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public CreateBankTransactionCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<string> Handle(CreateBankTransactionCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.BankTransaction entity = new Domain.Entities.BankTransaction
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