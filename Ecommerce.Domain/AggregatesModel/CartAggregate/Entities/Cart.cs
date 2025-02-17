namespace Ecommerce.Domain.AggregatesModel.CartAggregate.Entities;

public class Cart : AuditableEntity, IAggregateRoot
{
    public long UserId { get; set; }
    public List<CartItem> _cartItem = [];
    public IReadOnlyCollection<CartItem> CartItems => _cartItem.AsReadOnly();

    public Cart(long userId)
    {
        UserId = userId;
    }

    public static Cart Create(int userId) => new(userId);

    public void AddCartItem(CartItem cartItem) => _cartItem.Add(cartItem);

    public void ClearCart() => _cartItem.Clear();

    public void RemoveCartItem(CartItem cartItem) => _cartItem.Remove(cartItem);
}
