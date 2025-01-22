namespace Ecommerce.Domain.AggregatesModel.CartAggregate.Entities;

public class CartItem : Entity
{
    public new int Id { get; private set; }
    public int CartID { get; private set; }
    public int ProductId { get; private set; }
    public int Quantity { get; private set; }
    public DateTime AddedDate { get; private set; }
    public bool IsChecked { get; private set; }
    private CartItem(int cartId, int productId, int quantity)
    {
        CartID = Guard.Against.NegativeOrZero(cartId);
        ProductId = Guard.Against.NegativeOrZero(productId);
        Quantity = Guard.Against.NegativeOrZero(quantity);
        AddedDate = DateTime.UtcNow;
        IsChecked = true;
    }
    public static CartItem Create(int cartId, int productId, int quantity)
        => new(cartId, productId, quantity);

    public void ModifyIsChecked(bool check) => IsChecked = check;
}
