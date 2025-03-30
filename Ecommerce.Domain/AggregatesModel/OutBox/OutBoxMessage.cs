namespace Ecommerce.Domain.AggregatesModel.OutBox;

public class OutBoxMessage : IAggregateRoot
{
    public Guid Id { get; set; }
    public string EventType { get; set; } = default!;
    public string EventData { get; set; } = default!;
    public DateTime? ProcessedOnUtc { get; set; }
    public DateTime? OccuredOnUtc { get; set; }
    public string? Errors { get; set; }
}
