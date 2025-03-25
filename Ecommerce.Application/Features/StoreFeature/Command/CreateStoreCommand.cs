namespace Ecommerce.Application.Features.StoreFeature.Command;

public static class CreateStoreCommand
{
    #region Command
    public record Command(string StoreName, long AppUserId) : ICommand<BaseResult<Response>>;
    #endregion
    #region Validation
    public sealed class CreateStoreCommandValidator : AbstractValidator<Command>
    {
        public CreateStoreCommandValidator(IAppUserReadonlyRepository appUserReadonlyRepository)
        {
            RuleFor(x => x.StoreName).NotEmpty().WithMessage("Invalid store name");
            RuleFor(x => x.AppUserId)
                .MustAsync(appUserReadonlyRepository.StoreExistAsync)
                .WithMessage("User can have have one store.");
        }
    }
    #endregion

    #region Handler
    public sealed class Handler(IStoreRepository storeRepository)
        : ICommandHandler<Command, BaseResult<Response>>
    {
        public async Task<BaseResult<Response>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var store = Store.Create(request.StoreName, request.AppUserId);
                store.AddAdminstrator(request.AppUserId);
                await storeRepository.AddAsync(store, cancellationToken);
                await storeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
                return BaseResult<Response>.Ok(new Response(store.Guid));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
    #endregion

    #region Response
    public record Response(Guid StoreGuid);
    #endregion
}
