namespace Ecommerce.Domain.AggregatesModel.ProductConfirmAggregate;

public class ProductConfirm : Entity, IAggregateRoot
{
    public new int Id { get; private set; }
    public int ProductItemId { get; private set; }
    public int InvarientOptionId { get; private set; }
    private ProductConfirm(int productItemId, int invarientOptionId)
    {
        ProductItemId = productItemId;
        InvarientOptionId = invarientOptionId;
    }
    public static ProductConfirm Create(int productItemId, int invarientOptionId)
        => new(productItemId, invarientOptionId);

}
