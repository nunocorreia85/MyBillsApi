using System.Threading;
using System.Threading.Tasks;
using MyBills.Domain.Common;

namespace MyBills.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task PublishAsync(DomainEvent domainEvent, CancellationToken cancellationToken);
    }
}