namespace Ecommerce.Application.DomainEvents;

public class StoreCreatedEventHandler(
    ILogRepository logRepository,
    UserManager<AppUser> userManager,
    ILogger<StoreCreatedEventHandler> logger
)
    : EcommerceDomainEventBaseHandler<StoreCreatedEventHandler, StoreCreatedEvent>(
        logRepository,
        logger
    )
{
    public override async Task<EventLog> CreateLog(CancellationToken cancellationToken)
    {
        try
        {
            var description =
                $"Store create: User with Id {Notification.AppUserId} created store with id {Notification.StoreGuid}";
            var eventLog = new EventLog(
                EventType.StoreCreated,
                description,
                Notification.AppUserId
            );
            var user = await userManager.Users.FirstOrDefaultAsync(
                x => x.Id == Notification.AppUserId,
                cancellationToken
            );
            user?.UpdateStoreId(Notification.StoreGuid);
            await userManager.UpdateAsync(user);
            return eventLog;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
