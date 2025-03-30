namespace Ecommerce.Api.Models;

public record StoreBase : IMapFrom<GetStoreQuery.Response>
{
    public int MaxUser { get; set; }
    public string StoreName { get; set; } = default!;
}
