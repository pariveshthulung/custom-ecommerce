using Ecommerce.Domain.AggregatesModel.ProductAggregate.Events;

namespace Ecommerce.Application.Features.ProductFeature.Commands;

public static class AddProductCommand
{
    #region  Command
    public record Command : ICommand<BaseResult<Guid>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    #endregion
    #region Validation
    public class AddProductCommandValidation : AbstractValidator<Command>
    {
        public AddProductCommandValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        }
    }
    #endregion
    #region  Handler
    public class Handler(
        IProductRepository productRepository,
        ICurrentUserService currentUserService,
        IAppUserRepository appUserRepository
    ) : ICommandHandler<Command, BaseResult<Guid>>
    {
        public async Task<BaseResult<Guid>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            var currentUser = await appUserRepository.GetByEmailAsync(
                currentUserService.UserEmail,
                cancellationToken
            );
            var newProduct = Product.Create(
                request.Name,
                request.Description,
                currentUserService.UserEmail,
                currentUserService.UserId,
                currentUser.StoreId.GetValueOrDefault()
            );
            var product = await productRepository.AddAsync(newProduct, cancellationToken);
            await productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return BaseResult<Guid>.Ok(newProduct.Guid);
        }
    }
    #endregion
}
