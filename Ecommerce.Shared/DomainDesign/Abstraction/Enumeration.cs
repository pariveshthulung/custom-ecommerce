namespace Ecommerce.Shared.DomainDesign.Abstraction;

public class Enumeration : IComparable
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    public Enumeration(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public override bool Equals(object? obj)
    {
        if (obj is not Enumeration other)
            return false;
        var typeMatch = GetType().Equals(other.GetType());
        var valueMatch = Id.Equals(other.Id);

        return typeMatch && valueMatch;
    }
    public override int GetHashCode() => Id.GetHashCode();

    public static bool operator ==(Enumeration left, Enumeration right)
    {
        if (ReferenceEquals(left, right))
            return true;
        if (left is null || right is null)
            return false;
        return left.Equals(right);
    }

    public static bool operator !=(Enumeration left, Enumeration right) => !(left == right);

    public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
        typeof(T)
            .GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Select(x => x.GetValue(null)).Cast<T>();
    public static T FromName<T>(string name) where T : Enumeration => Parse<T, string>(name, "Name", item => item.Name == name);
    public static T FromValue<T>(int id) where T : Enumeration => Parse<T, int>(id, "Value", item => item.Id == id);
    public static T FromNameCaseInsensitive<T>(string name) where T : Enumeration => Parse<T, string>(name, "Name", item => item.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    private static T Parse<T, K>(K value, string discription, Func<T, bool> predicate) where T : Enumeration
    {
        var matchingValue = GetAll<T>().FirstOrDefault(predicate);
        return matchingValue != null ? matchingValue : throw new InvalidOperationException($"{value} is not a valid {discription} in {typeof(T)}");


    }
    public int CompareTo(object? obj) => Id.CompareTo(((Enumeration)obj).Id);

}
