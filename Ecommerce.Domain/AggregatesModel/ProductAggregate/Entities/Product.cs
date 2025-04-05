using Ecommerce.Domain.AggregatesModel.ProductAggregate.Events;

namespace Ecommerce.Domain.AggregatesModel.ProductAggregate.Entities;

public class Product : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public long StoreId { get; private set; }
    private List<ProductImage> _productImages = [];
    public IReadOnlyCollection<ProductImage> ProductImages => _productImages.AsReadOnly();
    private List<ProductItem> _productItems = [];
    public IReadOnlyCollection<ProductItem> ProductItems => _productItems.AsReadOnly();
    private List<long> _categoriesId = [];
    public IReadOnlyCollection<long> CategoriesId => _categoriesId.AsReadOnly();

    private Product() { }

    private Product(string name, string description, string userEmail, long appUserId, long storeId)
    {
        Name = Guard.Against.NullOrWhiteSpace(name);
        Description = Guard.Against.NullOrWhiteSpace(description);
        StoreId = Guard.Against.NegativeOrZero(storeId);
        AddDomainEvent(new ProductAddedEvent(this.Name, userEmail, appUserId, storeId));
    }

    public static Product Create(
        string name,
        string description,
        string userEmail,
        long userId,
        long storeId
    )
    {
        return new Product(name, description, userEmail, userId, storeId);
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void AddProductImage(ProductImage productImage) => _productImages.Add(productImage);

    public void AddProductItem(ProductItem productItem) => _productItems.Add(productItem);

    public void RemoveProductItem(ProductItem productItem) => _productItems.Remove(productItem);

    // public void AddCategory(Category category)
    // {
    //     if (!_categories.Contains(category))
    //         _categories.Add(category);
    // }
}
