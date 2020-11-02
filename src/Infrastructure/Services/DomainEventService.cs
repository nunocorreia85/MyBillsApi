using MediatR;
using Microsoft.Extensions.Logging;
using MyBills.Application.Common.Interfaces;
using MyBills.Application.Common.Models;
using MyBills.Domain.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyBills.Infrastructure.Services
{
    public class DomainEventService : IDomainEventService
    {
        private readonly ILogger<DomainEventService> _logger;
        private readonly IMediator _mediator;

        public DomainEventService(ILogger<DomainEventService> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task PublishAsync(DomainEvent domainEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Publishing domain event. Event - {event}", domainEvent.GetType().Name);
            await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent), cancellationToken);
        }

        private INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
        {
            return (INotification)Activator.CreateInstance(
                typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent);
        }
    }
}