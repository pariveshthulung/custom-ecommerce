namespace Ecommerce.Shared.DomainDesign.Extentions;
public static class EnumerationExtension
{
    public static T GetRandom<T>(this IEnumerable<T> enumeration)
    {
        if (!enumeration.Any())
            return default;
        Random random = new();
        var arr = enumeration.ToArray();
        var index = random.Next(arr.Length);
        return arr[index];
    }
}
