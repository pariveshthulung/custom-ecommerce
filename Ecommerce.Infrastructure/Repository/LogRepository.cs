namespace Ecommerce.Infrastructure.Repository;

public class LogRepository(EcommerceDbContext ecommerceDbContext, ILogger<LogRepository> logger)
    : ILogRepository
{
    public async Task<EventLog> CreateAsync(EventLog logEvent, CancellationToken cancellationToken)
    {
        try
        {
            if (!logEvent.IsTransient())
                throw new Exception("Could not create log.");

            var eventLog = await ecommerceDbContext.EventLogs.AddAsync(logEvent, cancellationToken);
            return eventLog.Entity;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Could not create log {@log}", logEvent);
            throw;
        }
    }
}
