namespace Ecommerce.Application.Common.Model;

public record ProductImageModel : IMapFrom<ProductImage>
{
    public long ProductId { get; set; }
    public string Image { get; set; } = default!;
}
