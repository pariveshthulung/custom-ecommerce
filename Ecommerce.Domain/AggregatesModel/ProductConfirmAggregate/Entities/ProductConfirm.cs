namespace Ecommerce.Domain.AggregatesModel.ProductConfirmAggregate.Entities;

public class ProductConfirm : Entity, IAggregateRoot
{
    public long ProductItemId { get; private set; }
    public long InvarientOptionId { get; private set; }

    private ProductConfirm(long productItemId, long invarientOptionId)
    {
        ProductItemId = productItemId;
        InvarientOptionId = invarientOptionId;
    }

    public static ProductConfirm Create(long productItemId, long invarientOptionId) =>
        new(productItemId, invarientOptionId);
}
