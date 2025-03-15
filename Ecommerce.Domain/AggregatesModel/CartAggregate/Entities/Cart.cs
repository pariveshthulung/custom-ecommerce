namespace Ecommerce.Domain.AggregatesModel.CartAggregate.Entities;

public class Cart : AuditableEntity, IAggregateRoot
{
    public long AppUserId { get; set; }
    public List<CartItem> _cartItem = [];
    public IReadOnlyCollection<CartItem> CartItems => _cartItem.AsReadOnly();

    public Cart(long appUserId)
    {
        AppUserId = appUserId;
    }

    public static Cart Create(int AppUserId) => new(AppUserId);

    public void AddCartItem(CartItem cartItem) => _cartItem.Add(cartItem);

    public void ClearCart() => _cartItem.Clear();

    public void RemoveCartItem(CartItem cartItem) => _cartItem.Remove(cartItem);
}
