using Ecommerce.Shared.DomainDesign.Abstraction;

namespace Ecommerce.Shared.DomainDesign.Extentions;

public static class MeditorExtension
{
    public static async Task PublicDomainEvent(this Mediator mediator, DbContext context)
    {
        var domainEntities = context.ChangeTracker.Entries<Entity>()
            .Where(x => x.Entity.DomainEvent != null && x.Entity.DomainEvent.Any());
        var domainEvents = domainEntities.SelectMany(x => x.Entity.DomainEvent).ToList();
        domainEntities.ToList().ForEach(e => e.Entity.ClearDomainEvent());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}
