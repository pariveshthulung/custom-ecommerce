using Ecommerce.Domain.AggregatesModel.ProductAggregate.Events;

namespace Ecommerce.Application.Features.ProductFeature.DomainEvents;

public class ProductUpdatedDomainEventHandler(
    ILogger<ProductUpdatedDomainEventHandler> logger,
    ILogRepository logRepository
)
    : EcommerceDomainEventBaseHandler<ProductUpdatedDomainEventHandler, ProductUpdatedEvent>(
        logRepository,
        logger
    )
{
    public override async Task<EventLog> CreateLog(CancellationToken cancellationToken)
    {
        try
        {
            var description =
                $"Product updated: {Notification.UserEmail} updated product {Notification.ProductName}";
            var eventlog = new EventLog(
                EventType.ProductUpdated,
                description,
                Notification.UserId,
                Notification.StoreId
            );

            return eventlog;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
