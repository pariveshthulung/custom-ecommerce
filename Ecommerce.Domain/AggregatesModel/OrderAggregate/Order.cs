namespace Ecommerce.Domain.AggregatesModel.OrderAggregate;

public class Order : Entity, IAggregateRoot
{
    public new int Id { get; private set; }
    public Decimal OrderTotal { get; private set; }
    public DateTime OrderedDate { get; private set; }
    public DateTime? PaymentDate { get; private set; }
    public string? TransactionCode { get; private set; }
    private List<OrderItem> _orderItems = [];
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
    private Order(
        decimal orderTotal,
        DateTime orderDate,
        DateTime paymentDate,
        string transactionCode)
    {
        OrderTotal = Guard.Against.NegativeOrZero(orderTotal);
        OrderedDate = orderDate;
        PaymentDate = paymentDate;
        TransactionCode = transactionCode;
    }
    public static Order Create(
        decimal orderTotal,
        DateTime orderDate,
        DateTime paymentDate,
        string transactionCode)
    => new(orderTotal, orderDate, paymentDate, transactionCode);
    public void AddOrderItem(decimal price, int quantity)
    {
        var orderItem = OrderItem.Create(Id, price, quantity);
        _orderItems.Add(orderItem);
    }

}
