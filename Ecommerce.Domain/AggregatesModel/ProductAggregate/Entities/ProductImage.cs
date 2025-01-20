namespace Ecommerce.Domain.AggregatesModel.ProductAggregate.Entities;

public class ProductImage : Entity
{
    private int _id;
    public virtual new int Id
    {
        get { return _id; }
        private set { _id = value; }
    }
    public int ProductId { get; private set; }
    public string Image { get; private set; } = null!;
    private ProductImage(int productId, string image)
    {
        ProductId = Guard.Against.NegativeOrZero(productId);
        Image = Guard.Against.NullOrWhiteSpace(image);
    }

    public static ProductImage Create(int productId, string image)
    {
        return new ProductImage(productId, image);
    }
}
