namespace Ecommerce.Domain.AggregatesModel.EventAggregate.Entities;

public class EventLog : AuditableEntity, IAggregateRoot
{
    public long AppUserId { get; set; }
    public EventType EventType { get; set; } = default!;
    public string Description { get; set; } = default!;

    public EventLog() { }

    public EventLog(EventType eventType, string description, long appUserId)
    {
        EventType = eventType;
        Description = description;
        AppUserId = appUserId;
        AddedBy = appUserId;
        AddedOn = DateTime.UtcNow;
        IsActive = true;
    }
}
