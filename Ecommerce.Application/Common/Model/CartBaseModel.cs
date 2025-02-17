namespace Ecommerce.Application.Common.Model;

public class CartDto : IMapFrom<Cart>
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public int UserId { get; set; }
    public List<CartItemDto> CartItems { get; set; } = [];
}

public class CartItemDto
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public int CartID { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime AddedDate { get; set; }
    public bool IsChecked { get; set; }
}

public class CreateCartItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }

}

public class UpdateCartDto
{
    public int UserId { get; set; }
    public List<CartItemDto> CartItems { get; set; } = [];
}
public class UpdateCartItemDto
{
    public Guid Guid { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public bool IsChecked { get; set; }
}