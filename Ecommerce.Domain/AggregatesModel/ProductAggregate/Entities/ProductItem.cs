namespace Ecommerce.Domain.AggregatesModel.ProductAggregate.Entities;

public class ProductItem : Entity
{
    private int _id;
    public virtual new int Id
    {
        get { return _id; }
        set { _id = value; }
    }
    public string Image { get; private set; } = default!;
    public int ProductId { get; private set; }
    public string SKU { get; private set; } = default!;
    public int Quantity { get; private set; }
    public Decimal Price { get; private set; }
    private ProductItem(string image, int productId, string sku, int quantity, Decimal price)
    {
        Image = Guard.Against.NullOrWhiteSpace(image);
        ProductId = Guard.Against.NegativeOrZero(productId);
        SKU = Guard.Against.NullOrWhiteSpace(sku);
        Quantity = Guard.Against.NegativeOrZero(quantity);
        Price = Guard.Against.NegativeOrZero(price);
    }
    public static ProductItem Create(string image, int productId, string sku, int quantity, Decimal price)
    {
        return new ProductItem(image, productId, sku, quantity, price);
    }
}
