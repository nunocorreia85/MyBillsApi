using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Application.BankTransactions.Commands.CreateBankTransaction
{
    public class CreateBankTransactionCommand : IRequest<int>
    {
        public string Memo { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public int AccountId { get; set; }
        public string UserName { get; set; }
    }

    public class CreateBankTransactionCommandHandler : IRequestHandler<CreateBankTransactionCommand, int>
    {        
        public Task<int> Handle(CreateBankTransactionCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(1);
        }
    }
}