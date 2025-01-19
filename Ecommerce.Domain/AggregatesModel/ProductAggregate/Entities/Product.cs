using Ecommerce.Shared.DomainDesign.Abstraction;
using static Ecommerce.Shared.DomainDesign.Abstraction.Entity;

namespace Ecommerce.Domain.AggregatesModel.ProductAggregate.Entities;

public class Product : AuditableEntity, IAggregateRoot
{
    private int _id;
    public virtual new int Id
    {
        get { return _id; }
        private set { _id = value; }
    }
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    private List<ProductImage> _productImages = [];
    public IReadOnlyCollection<ProductImage> ProductImages => _productImages.AsReadOnly();
    private List<ProductItem> _productItems = [];
    public IReadOnlyCollection<ProductItem> ProductItems => _productItems.AsReadOnly();
    private Product(string name, string description)
    {
        Name = Guard.Against.NullOrWhiteSpace(name);
        Description = Guard.Against.NullOrWhiteSpace(description);
    }
    public static Product Create(string name, string description)
    {
        return new Product(name, description);
    }
    public void AddProductImage(string image)
    {
        ProductImage productImage = ProductImage.Create(this.Id, image);
        _productImages.Add(productImage);
    }
    public void AddProductItem(string image, string sku, int quantity, decimal price)
    {
        ProductItem productItem = ProductItem.Create(image, this.Id, sku, quantity, price);
        _productItems.Add(productItem);
    }
}
