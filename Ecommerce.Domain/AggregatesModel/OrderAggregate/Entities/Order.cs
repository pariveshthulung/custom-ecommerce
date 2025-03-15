namespace Ecommerce.Domain.AggregatesModel.OrderAggregate.Entities;

public class Order : Entity, IAggregateRoot
{
    public decimal? OrderTotal { get; private set; }
    public long AppUserId { get; private set; }
    public DateTime OrderedDate { get; private set; }
    public DateTime? PaymentDate { get; private set; }
    public string? TransactionCode { get; private set; }
    private List<OrderItem> _orderItems = [];
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    protected Order() { }

    private Order(long appUserId, DateTime orderDate, DateTime? paymentDate, string transactionCode)
    {
        AppUserId = Guard.Against.NegativeOrZero(appUserId);
        OrderedDate = orderDate;
        PaymentDate = paymentDate;
        TransactionCode = transactionCode;
    }

    public static Order Create(
        long customerId,
        DateTime orderDate,
        DateTime? paymentDate,
        string transactionCode
    ) => new(customerId, orderDate, paymentDate, transactionCode);

    public void AddOrderItem(OrderItem orderItem)
    {
        _orderItems.Add(orderItem);
    }
}
