namespace Ecommerce.Domain.AggregatesModel.ProductAggregate.Events;

public record ProductAddedEvent(string ProductName, string UserEmail, long UserId, long StoreId)
    : INotification;
