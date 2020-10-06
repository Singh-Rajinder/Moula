using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Moula.Application.Payments.Events
{
    public class PaymentEventsHandler: 
        INotificationHandler<PaymentCreated>, 
        INotificationHandler<PaymentClosed>, 
        INotificationHandler<PaymentCancelled>, 
        INotificationHandler<PaymentProcessed>
    {
        public Task Handle(PaymentCreated notification, CancellationToken cancellationToken)
        {
            // TODO: Save event to event bus like Kafka, RabbitMq etc...
            return Task.CompletedTask;
        }

        public Task Handle(PaymentClosed notification, CancellationToken cancellationToken)
        {
            // TODO: Save event to event bus like Kafka, RabbitMq etc...
            return Task.CompletedTask;
        }

        public Task Handle(PaymentCancelled notification, CancellationToken cancellationToken)
        {
            // TODO: Save event to event bus like Kafka, RabbitMq etc...
            return Task.CompletedTask;
        }

        public Task Handle(PaymentProcessed notification, CancellationToken cancellationToken)
        {
            // TODO: Save event to event bus like Kafka, RabbitMq etc...
            return Task.CompletedTask;
        }
    }
}
