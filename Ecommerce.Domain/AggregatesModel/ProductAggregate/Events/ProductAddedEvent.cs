namespace Ecommerce.Domain.AggregatesModel.ProductAggregate.Events;

public record ProductAddedEvent(string UserEmail, string ProductName, long AppUserId)
    : INotification;
