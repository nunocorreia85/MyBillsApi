using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyBills.Application.Common.Interfaces;
using MyBills.Application.Shared.BankTransactions.Commands;
using MyBills.Domain.Entities;
using MyBills.Domain.Events;

namespace MyBills.Application.BankTransactions.Commands.CreateBankTransaction
{
    public class CreateBankTransactionCommandHandler : IRequestHandler<CreateBankTransactionCommand, long>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public CreateBankTransactionCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
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

            await _applicationDbContext.BankTransactions.AddAsync(entity, cancellationToken);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}