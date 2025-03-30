namespace Ecommerce.Domain.AggregatesModel.StoreAggregate.Events;

public record StoreUpdatedEvent(long AppUserId, string UserEmail, string OldName, string NewName)
    : INotification;
