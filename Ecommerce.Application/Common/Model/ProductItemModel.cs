namespace Ecommerce.Application.Common.Model;

public record ProductItemModel : IMapFrom<ProductItem>
{
    public Guid Guid { get; set; }
    public string Image { get; set; } = default!;
    public long ProductId { get; set; }
    public string SKU { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
