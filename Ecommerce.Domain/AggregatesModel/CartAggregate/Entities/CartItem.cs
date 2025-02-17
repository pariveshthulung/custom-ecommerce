namespace Ecommerce.Domain.AggregatesModel.CartAggregate.Entities;

public class CartItem : Entity
{
    public long CartId { get; private set; }
    public long ProductId { get; private set; }
    public int Quantity { get; private set; }
    public DateTime AddedDate { get; private set; }
    public bool IsChecked { get; private set; }

    private CartItem(long productId, int quantity)
    {
        ProductId = Guard.Against.NegativeOrZero(productId);
        Quantity = Guard.Against.NegativeOrZero(quantity);
        AddedDate = DateTime.UtcNow;
        IsChecked = true;
    }

    public static CartItem Create(long productId, int quantity) => new(productId, quantity);

    public void AddQuantity(int quantity) => Quantity += quantity;

    public void SubtractQuantity(int quantity) => Quantity -= quantity;

    public void ModifyIsChecked(bool check) => IsChecked = check;
}
