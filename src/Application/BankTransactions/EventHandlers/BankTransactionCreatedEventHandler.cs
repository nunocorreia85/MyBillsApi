using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyBills.Application.Common.Interfaces;
using MyBills.Application.Common.Models;
using MyBills.Domain.Events;

namespace MyBills.Application.BankTransactions.EventHandlers
{
    public class BankTransactionCreatedEventHandler
        : INotificationHandler<DomainEventNotification<BankTransactionCreatedEvent>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<BankTransactionCreatedEventHandler> _logger;

        public BankTransactionCreatedEventHandler(ILogger<BankTransactionCreatedEventHandler> logger,
            IApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task Handle(DomainEventNotification<BankTransactionCreatedEvent> notification,
            CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            _logger.LogInformation("MyBills Domain Event: {DomainEvent} Date Occured: {dateOccured}",
                domainEvent.GetType().Name, domainEvent.DateOccurred);

            var account = await _dbContext.Accounts.FirstAsync(a => a.Id == domainEvent.AccountId,
                cancellationToken);
            account.Balance += domainEvent.Amount;
        }
    }
}