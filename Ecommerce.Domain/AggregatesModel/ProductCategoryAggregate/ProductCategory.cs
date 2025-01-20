namespace Ecommerce.Domain.AggregatesModel.ProductCategoryAggregate;

public class ProductCategory : Entity, IAggregateRoot
{
    public new int Id { get; private set; }
    public int ProductId { get; private set; }
    public int CategoryId { get; private set; }
    private ProductCategory(int categoryId, int productId)
    {
        ProductId = Guard.Against.NegativeOrZero(productId);
        CategoryId = Guard.Against.NegativeOrZero(categoryId);
    }
    public static ProductCategory Create(int categoryId, int productId)
        => new(categoryId, productId);

}
