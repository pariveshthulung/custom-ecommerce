using Ecommerce.Domain.AggregatesModel.AppUserAggregate.Events;

namespace Ecommerce.Application.DomainEvents;

public class AppUserRegisteredEventHandler(
    ILogger<AppUserRegisteredEventHandler> logger,
    ILogRepository logRepository,
    ICartRepository cartRepository,
    IAppUserRepository appUserRepository
)
    : EcommerceDomainEventBaseHandler<AppUserRegisteredEventHandler, AppUserRegisteredEvent>(
        logRepository,
        logger
    )
{
    public override async Task<EventLog> CreateLog(CancellationToken cancellationToken)
    {
        var user = await appUserRepository.GetByGuidAsync(Notification.UserGuid, cancellationToken);
        await cartRepository.AddAsync(Cart.Create(user.Id), cancellationToken);
        var description = $"User added : User with {Notification.NewUserEmail} has been added";
        var eventLog = new EventLog(
            EventType.ProductAdded,
            description,
            Notification.AppUserId,
            Notification.StoreId
        );
        return eventLog;
    }
}
