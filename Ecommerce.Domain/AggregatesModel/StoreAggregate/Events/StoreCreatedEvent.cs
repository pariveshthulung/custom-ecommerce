namespace Ecommerce.Domain.AggregatesModel.StoreAggregate.Events;

public record StoreCreatedEvent(long AppUserId, Guid StoreGuid) : INotification;
