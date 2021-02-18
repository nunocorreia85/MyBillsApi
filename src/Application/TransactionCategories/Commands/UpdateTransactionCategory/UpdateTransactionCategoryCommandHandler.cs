using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBills.Application.Common.Interfaces;
using MyBills.Application.Shared.TransactionCategories.Commands;

namespace MyBills.Application.TransactionCategories.Commands.UpdateTransactionCategory
{
    public class UpdateTransactionCategoryCommandHandler : IRequestHandler<UpdateTransactionCategoryCommand, long>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateTransactionCategoryCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<long> Handle(UpdateTransactionCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.TransactionCategories
                .FirstAsync(t => t.Id == request.Id, cancellationToken);

            entity.Description = request.Description;
            entity.Name = request.Name;
            entity.RecurringPeriod = request.RecurringPeriod;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}