namespace Ecommerce.Domain.AggregatesModel.ProductAggregate.Events;

public record ProductUpdatedEvent(string ProductName, string UserEmail, long UserId, long StoreId)
    : INotification;
