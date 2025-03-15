namespace Ecommerce.Domain.AggregatesModel.AppUserAggregate;

public class Address : ValueObject
{
    public string City { get; private set; } = default!;
    public string AddressLine { get; private set; } = default!;
    public int StreetNo { get; private set; }
    public string Region { get; private set; } = default!;
    public string PostalCode { get; private set; } = default!;
    public bool IsDefault { get; private set; }

    public Address(
        string city,
        string addressLine,
        int streetNo,
        string region,
        string postalCode,
        bool isDefault
    )
    {
        City = city;
        AddressLine = addressLine;
        StreetNo = streetNo;
        Region = region;
        PostalCode = postalCode;
        IsDefault = isDefault;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return City;
        yield return AddressLine;
        yield return StreetNo;
        yield return Region;
        yield return PostalCode;
        yield return IsDefault;
    }
}
