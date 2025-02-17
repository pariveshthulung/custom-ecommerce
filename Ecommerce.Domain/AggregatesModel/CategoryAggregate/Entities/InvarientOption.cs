namespace Ecommerce.Domain.AggregatesModel.CategoryAggregate.Entities;

public class InvarientOption : Entity
{
    public long InvarientId { get; private set; }
    public string Value { get; private set; } = default!;

    private InvarientOption(long invarientId, string value)
    {
        InvarientId = Guard.Against.NegativeOrZero(invarientId);
        Value = Guard.Against.NullOrWhiteSpace(value);
    }

    public static InvarientOption Create(long invarientId, string value) => new(invarientId, value);
}
