using AutoMapper;
using Ecommerce.Application.Common.Model;
using Ecommerce.Application.Common.Repository;
using Ecommerce.Shared.Helper;

namespace Ecommerce.Application.Features.Carts.Query.Get;

public class GetCartQueryHandler(ICartRepository cartRepository, ICurrentUserProvider userProvider, IMapper mapper)
    : IQueryHandler<GetCartQuery, BaseResult<CartDto>>
{
    public async Task<BaseResult<CartDto>> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await userProvider.GetCurrentUser();
        var cart = await cartRepository.GetByUserIdAsync(currentUser.Id, cancellationToken);
        return BaseResult<CartDto>.Ok(mapper.Map<CartDto>(cart));
    }
}
