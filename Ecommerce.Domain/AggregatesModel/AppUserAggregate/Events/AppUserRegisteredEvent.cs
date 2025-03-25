namespace Ecommerce.Domain.AggregatesModel.AppUserAggregate.Events;

public record AppUserRegisteredEvent(EventType EventType, string Description, long AppUserId)
    : INotification;
