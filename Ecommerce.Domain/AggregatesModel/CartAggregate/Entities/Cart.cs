namespace Ecommerce.Domain.AggregatesModel.CartAggregate.Entities;

public class Cart : AuditableEntity, IAggregateRoot
{
    public new int Id { get; private set; }
    public int UserId { get; set; }
    public List<CartItem> _cartItem = [];
    public IReadOnlyCollection<CartItem> CartItems => _cartItem.AsReadOnly();
    private Cart(int userId)
    {
        UserId = userId;
    }
    public static Cart Create(int userId) => new(userId);
    public void AddCartItem(CartItem cartItem) => _cartItem.Add(cartItem);
    public void ClearCart() => _cartItem.Clear();
    public void RemoveCartItem(CartItem cartItem) => _cartItem.Remove(cartItem);

}
