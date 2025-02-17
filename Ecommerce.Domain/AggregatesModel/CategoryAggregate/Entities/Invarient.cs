namespace Ecommerce.Domain.AggregatesModel.CategoryAggregate.Entities;

public class Invarient : Entity
{
    public string Name { get; private set; }
    public long CategoryId { get; private set; }
    private List<InvarientOption> _invarientOption = [];
    public IReadOnlyCollection<InvarientOption> InvarientOptions => _invarientOption.AsReadOnly();

    private Invarient(long categoryId, string name)
    {
        CategoryId = Guard.Against.NegativeOrZero(categoryId);
        Name = Guard.Against.NullOrWhiteSpace(name);
    }

    public static Invarient Create(long categoryId, string name) => new(categoryId, name);

    public void AddInvarientOption(InvarientOption invarientOption)
    {
        _invarientOption.Add(invarientOption);
    }
}
