using Ecommerce.Shared.DomainDesign.Abstraction;

namespace Ecommerce.Shared.Extension;

public static class MediatorExtension
{
    public static async Task DispatchDomainEventAsync(this IMediator mediator, DbContext dbContext)
    {
        var domainEntities = dbContext
            .ChangeTracker.Entries<Entity>()
            .Where(e => e.Entity.DomainEvent != null && e.Entity.DomainEvent.Any());

        var domainEvents = domainEntities.SelectMany(e => e.Entity.DomainEvent).ToList();

        domainEntities.ToList().ForEach(e => e.Entity.ClearDomainEvent());
        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}
