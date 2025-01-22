namespace Ecommerce.Domain.AggregatesModel.OrderAggregate.Entities;

public class OrderItem : Entity
{
    public new int Id { get; private set; }
    public int OrderId { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public OrderItem(int orderId, decimal price, int quantity)
    {
        OrderId = Guard.Against.NegativeOrZero(orderId);
        Price = Guard.Against.NegativeOrZero(price);
        Quantity = Guard.Against.NegativeOrZero(quantity);
    }
    public static OrderItem Create(int orderId, decimal price, int quantity)
        => new(orderId, price, quantity);
}
