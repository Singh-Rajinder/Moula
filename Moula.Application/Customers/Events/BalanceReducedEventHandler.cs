using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Moula.Application.Customers.Events
{
    public class BalanceReducedEventHandler: INotificationHandler<BalanceReduced>
    {
        public Task Handle(BalanceReduced notification, CancellationToken cancellationToken)
        {
            // TODO: Save event to event bus like Kafka, RabbitMq etc...
            return Task.CompletedTask;
        }
    }
}
