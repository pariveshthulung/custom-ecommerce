namespace Ecommerce.Domain.AggregatesModel.CategoryAggregate.Entities;

public class Category : Entity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    private List<Invarient> _invarients = [];
    public IReadOnlyCollection<Invarient> Invarients => _invarients.AsReadOnly();

    private Category(string name)
    {
        Name = name;
    }

    public static Category Create(string name) => new(name);

    public void AddInvarient(Invarient invarient)
    {
        _invarients.Add(invarient);
    }
}
