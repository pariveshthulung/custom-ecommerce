namespace Ecommerce.Application.DomainEvents.Base;

public class EcommerceDomainEventBaseHandler<T, N>(ILogRepository logRepository, ILogger<T> logger)
    : INotificationHandler<N>
    where T : class
    where N : INotification
{
    public N Notification = default!;

    public async Task Handle(N notification, CancellationToken cancellationToken)
    {
        try
        {
            Notification = notification;
            var log = await CreateLog(cancellationToken);
            await logRepository.CreateAsync(log, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "@{notification}", notification);
            throw;
        }
    }

    public virtual Task<EventLog> CreateLog(CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Child class must implement.");
    }
}
