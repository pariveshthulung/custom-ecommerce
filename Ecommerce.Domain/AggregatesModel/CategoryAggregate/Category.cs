namespace Ecommerce.Domain.AggregatesModel.CategoryAggregate;

public class Category : Entity, IAggregateRoot
{
    public new int Id { get; private set; }
    public string Name { get; private set; } = default!;
    private List<Invarient> _invarients = [];
    public IReadOnlyCollection<Invarient> Invarients => _invarients.AsReadOnly();
    private Category(string name)
    {
        Name = name;
    }
    public static Category Create(string name)
        => new(name);
    public void AddInvarient(string name)
    {
        var invarient = Invarient.Create(this.Id, name);
        _invarients.Add(invarient);
    }

}
