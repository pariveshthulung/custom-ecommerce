using Ecommerce.Shared.Wrappers;

namespace Ecommerce.Application.Features.Carts.Query.Get;

public class GetCartQueryHandler(
    ICartRepository cartRepository,
    ICurrentUserService currentUserService,
    IMapper mapper
) : IQueryHandler<GetCartQuery, BaseResult<CartDto>>
{
    public async Task<BaseResult<CartDto>> Handle(
        GetCartQuery request,
        CancellationToken cancellationToken
    )
    {
        var cart = await cartRepository.GetByUserIdAsync(
            currentUserService.UserId,
            cancellationToken
        );
        return BaseResult<CartDto>.Ok(mapper.Map<CartDto>(cart));
    }
}
