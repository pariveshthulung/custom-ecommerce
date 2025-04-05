using Ecommerce.Domain.AggregatesModel.ProductAggregate.Events;

namespace Ecommerce.Application.DomainEvents;

public class ProductAddedEventHandler(
    ILogRepository logRepository,
    IStoreRepository storeRepository,
    ILogger<ProductAddedEventHandler> logger
)
    : EcommerceDomainEventBaseHandler<ProductAddedEventHandler, ProductAddedEvent>(
        logRepository,
        logger
    )
{
    public override async Task<EventLog> CreateLog(CancellationToken cancellationToken)
    {
        try
        {
            // var product = await productRepository.GetByGuidAsync(
            //     Notification.ProductGuid,
            //     cancellationToken
            // );
            var description =
                $"Product Added: {Notification.UserEmail} added new product {Notification.ProductName}";
            var eventLog = new EventLog(
                EventType.ProductAdded,
                description,
                Notification.UserId,
                Notification.StoreId
            );
            return eventLog;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
