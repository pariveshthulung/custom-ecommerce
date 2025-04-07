namespace Ecommerce.Domain.AggregatesModel.AppUserAggregate.Events;

public record AppUserRegisteredEvent(
    string NewUserEmail,
    Guid UserGuid,
    EventType EventType,
    long AppUserId,
    long? StoreId
) : INotification;
