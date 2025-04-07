namespace Ecommerce.Infrastructure.Interceptors;

public sealed class ConvertDomainEventIntoOutboxMessageInterceptor(
    ILogger<ConvertDomainEventIntoOutboxMessageInterceptor> logger
) : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default
    )
    {
        DbContext? dbContext = eventData.Context;
        if (dbContext is null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        var domainEntities = dbContext
            .ChangeTracker.Entries<IHasDomainEvents>()
            .Where(e => e.Entity.DomainEvent != null && e.Entity.DomainEvent.Any());

        var outBoxMessages = domainEntities
            .SelectMany(e => e.Entity.DomainEvent)
            .Select(domainEvent => new OutBoxMessage
            {
                Id = Guid.NewGuid(),
                OccuredOnUtc = DateTime.UtcNow,
                EventType = domainEvent.GetType().Name,
                EventData = JsonConvert.SerializeObject(
                    domainEvent,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }
                )
            })
            .ToList();
        dbContext.Set<OutBoxMessage>().AddRange(outBoxMessages);
        _EnsureEnumerationUnchanged(dbContext);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void _EnsureEnumerationUnchanged(DbContext dbContext)
    {
        //Enumerations should be changed in the db directly as a short term fix
        foreach (var entry in dbContext.ChangeTracker.Entries<Enumeration>())
        {
            if (entry.State != EntityState.Unchanged)
            {
                logger.LogInformation(
                    "An attempt to add an enumeration {FullName} entity. This must be manually inserted into the db",
                    entry.Entity.GetType().FullName
                );

                //Updated added state to modified so we can preserve tracking
                if (entry.State == EntityState.Added)
                    entry.State = EntityState.Modified;
                else
                    entry.State = EntityState.Unchanged;
            }
        }
    }
}
