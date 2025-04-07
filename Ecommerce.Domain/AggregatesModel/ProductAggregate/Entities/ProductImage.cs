namespace Ecommerce.Domain.AggregatesModel.ProductAggregate.Entities;

public class ProductImage : Entity
{
    public long ProductId { get; private set; }
    public string Image { get; private set; } = default!;

    private ProductImage(long productId, string image)
    {
        ProductId = Guard.Against.NegativeOrZero(productId);
        Image = Guard.Against.NullOrWhiteSpace(image);
    }

    public static ProductImage Create(long productId, string image)
    {
        return new ProductImage(productId, image);
    }
}
