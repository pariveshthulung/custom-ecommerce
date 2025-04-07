namespace Ecommerce.Application.Common.Model;

public record ProductModel : IMapFrom<Product>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public long StoreId { get; set; }
    public IEnumerable<ProductImageModel> ProductImages { get; set; } = default!;
    public IEnumerable<ProductItemModel> ProductItems { get; set; } = default!;
}
