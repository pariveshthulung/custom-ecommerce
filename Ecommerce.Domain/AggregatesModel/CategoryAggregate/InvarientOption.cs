namespace Ecommerce.Domain.AggregatesModel.CategoryAggregate;

public class InvarientOption : Entity
{
    public new int Id { get; private set; }
    public int InvarientId { get; private set; }
    public string Value { get; private set; } = default!;
    private InvarientOption(int invarientId, string value)
    {
        InvarientId = Guard.Against.NegativeOrZero(invarientId);
        Value = Guard.Against.NullOrWhiteSpace(value);
    }

    public static InvarientOption Create(int invarientId, string value)
        => new(invarientId, value);

}
