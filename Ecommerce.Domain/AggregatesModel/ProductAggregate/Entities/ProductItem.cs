namespace Ecommerce.Domain.AggregatesModel.ProductAggregate.Entities;

public class ProductItem : Entity
{
    public string Image { get; private set; } = default!;
    public long ProductId { get; private set; }
    public string SKU { get; private set; } = default!;
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }

    private ProductItem(string image, string sku, int quantity, decimal price)
    {
        Image = Guard.Against.NullOrWhiteSpace(image);
        SKU = Guard.Against.NullOrWhiteSpace(sku);
        Quantity = Guard.Against.NegativeOrZero(quantity);
        Price = Guard.Against.NegativeOrZero(price);
    }

    public static ProductItem Create(string image, string sku, int quantity, decimal price)
    {
        return new ProductItem(image, sku, quantity, price);
    }

    public void UpdateProductItem(string image, string sku, int quantity, decimal price)
    {
        Image = Guard.Against.NullOrWhiteSpace(image);
        SKU = Guard.Against.NullOrWhiteSpace(sku);
        Quantity = Guard.Against.NegativeOrZero(quantity);
        Price = Guard.Against.NegativeOrZero(price);
    }
}
