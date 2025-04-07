using Ecommerce.Application.Common.Model;

namespace Ecommerce.Api.Models;

public record ProductImageDto : IMapFrom<ProductImageModel>
{
    public long ProductId { get; set; }
    public string Image { get; set; } = default!;
}
