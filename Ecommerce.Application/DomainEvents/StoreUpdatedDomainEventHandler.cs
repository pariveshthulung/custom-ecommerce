namespace Ecommerce.Application.DomainEvents;

public class StoreUpdatedDomainEventHandler(
    ILogRepository LogRepository,
    ILogger<StoreUpdatedDomainEventHandler> Logger
)
    : EcommerceDomainEventBaseHandler<StoreUpdatedDomainEventHandler, StoreUpdatedEvent>(
        LogRepository,
        Logger
    )
{
    public override async Task<EventLog> CreateLog(CancellationToken cancellationToken)
    {
        var description =
            $"Store updated: {Notification.UserEmail} has updated store from {Notification.OldName} to {Notification.NewName}";
        return new EventLog(EventType.StoreUpdated, description, Notification.AppUserId);
    }
}
