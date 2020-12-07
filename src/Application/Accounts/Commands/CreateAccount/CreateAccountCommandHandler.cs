using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyBills.Application.Common.Interfaces;
using MyBills.Application.Shared.Accounts.Commands.CreateAccount;
using MyBills.Domain.Entities;

namespace MyBills.Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, long>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public CreateAccountCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<long> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var entity = new Account
            {
                ExternalId = request.ExternalId,
                Balance = request.Balance,
                BankAccountNumber = request.BankAccountNumber
            };

            await _applicationDbContext.Accounts.AddAsync(entity, cancellationToken);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}