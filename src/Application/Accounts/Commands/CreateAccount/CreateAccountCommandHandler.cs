using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyBills.Application.Common.Interfaces;
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
                Balance = request.Balance,
                BankAccountNumber = request.BankAccountNumber,
                OwnerName = request.OwnerName
            };

            _applicationDbContext.Accounts.Add(entity);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}