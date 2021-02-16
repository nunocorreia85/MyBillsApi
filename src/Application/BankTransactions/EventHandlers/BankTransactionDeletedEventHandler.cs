using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyBills.Application.Common.Interfaces;
using MyBills.Application.Common.Models;
using MyBills.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace MyBills.Application.BankTransactions.EventHandlers
{
    public class BankTransactionDeletedEventHandler
        : INotificationHandler<DomainEventNotification<BankTransactionDeletedEvent>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<BankTransactionDeletedEventHandler> _logger;

        public BankTransactionDeletedEventHandler(ILogger<BankTransactionDeletedEventHandler> logger,
            IApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task Handle(DomainEventNotification<BankTransactionDeletedEvent> notification,
            CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            _logger.LogInformation("MyBills Domain Event: {DomainEvent} Date Occured: {dateOccured}",
                domainEvent.GetType().Name, domainEvent.DateOccurred);

            var account = await _dbContext.Accounts.FirstAsync(a => a.Id == domainEvent.AccountId,
                cancellationToken);
            account.Balance -= domainEvent.Amount;
        }
    }
}