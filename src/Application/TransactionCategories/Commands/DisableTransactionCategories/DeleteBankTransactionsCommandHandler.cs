using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBills.Application.Common.Exceptions;
using MyBills.Application.Common.Interfaces;
using MyBills.Domain.Entities;

namespace MyBills.Application.TransactionCategories.Commands.DisableTransactionCategories
{
    public class DisableTransactionCategoriesCommandHandler : IRequestHandler<DisableTransactionCategoriesCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DisableTransactionCategoriesCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Unit> Handle(DisableTransactionCategoriesCommand request, CancellationToken cancellationToken)
        {
            var categories = await _applicationDbContext.TransactionCategories
                .Where(transaction => request.Ids.Contains(transaction.Id))
                .ToListAsync(cancellationToken);

            if (categories == null)
                throw new NotFoundException(nameof(BankTransaction), string.Join(",", request.Ids));

            categories.ForEach(transaction => transaction.Disabled = true);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}