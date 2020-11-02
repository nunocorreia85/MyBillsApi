using MyBills.Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace MyBills.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task PublishAsync(DomainEvent domainEvent, CancellationToken cancellationToken);
    }
}
