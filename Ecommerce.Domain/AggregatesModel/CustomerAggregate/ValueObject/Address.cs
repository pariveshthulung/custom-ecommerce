namespace Ecommerce.Domain.AggregatesModel.CustomerAggregate;

public class Address : ValueObject
{
    public int UserId { get; private set; }
    public string City { get; private set; } = default!;
    public string AddressLine { get; private set; } = default!;
    public int StreetNo { get; private set; }
    public string Region { get; private set; } = default!;
    public string PostalCode { get; private set; } = default!;
    public bool IsDefault { get; private set; }
    public Address(
        int userId,
        string city,
        string addressLine,
        int streetNo,
        string region,
        string postalCode,
        bool isDefault)
    {
        UserId = userId;
        City = city;
        AddressLine = addressLine;
        StreetNo = streetNo;
        Region = region;
        PostalCode = postalCode;
        IsDefault = isDefault;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return UserId;
        yield return City;
        yield return AddressLine;
        yield return StreetNo;
        yield return Region;
        yield return PostalCode;
        yield return IsDefault;
    }
}
