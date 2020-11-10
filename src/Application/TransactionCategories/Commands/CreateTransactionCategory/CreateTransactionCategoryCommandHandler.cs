using MediatR;
using MyBills.Application.Common.Interfaces;
using MyBills.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyBills.Application.TransactionCategories.Commands.CreateTransactionCategory
{
    public class CreateTransactionCategoryCommandHandler : IRequestHandler<CreateTransactionCategoryCommand, long>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateTransactionCategoryCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<long> Handle(CreateTransactionCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = new TransactionCategory()
            {
                AccountId = request.AccountId,
                Description = request.Description,
                Name = request.Name,
                RecurringPeriod = request.RecurringPeriod
            };

            await _dbContext.TransactionCategories.AddAsync(entity, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}