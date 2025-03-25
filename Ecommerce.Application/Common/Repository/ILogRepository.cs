namespace Ecommerce.Application.Common.Repository;

public interface ILogRepository
{
    Task<EventLog> CreateAsync(EventLog logEvent, CancellationToken cancellationToken);
}
