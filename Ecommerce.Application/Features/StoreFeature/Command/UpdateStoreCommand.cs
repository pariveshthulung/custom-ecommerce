namespace Ecommerce.Application.Features.StoreFeature.Command;

public static class UpdateStoreCommand
{
    #region Command
    public record Command(Guid StoreGuid, string StoreName, long AppuserId, string UserEmail)
        : ICommand<BaseResult<Response>>;
    #endregion
    #region Validation
    public class Validation : AbstractValidator<Command>
    {
        public Validation(IReadonlyStoreRepository readonlyStoreRepository)
        {
            RuleFor(x => x.StoreName).NotEmpty().WithMessage("Invalid store name");
            RuleFor(x => x.StoreGuid)
                .NotEmpty()
                .WithMessage("Invalid store")
                .MustAsync(readonlyStoreRepository.Exist)
                .WithMessage("Store does not exist");
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
                var store = await storeRepository.GetByGuidAsync(
                    request.StoreGuid,
                    cancellationToken
                );
                store!.Update(request.StoreName, request.AppuserId, request.UserEmail);
                storeRepository.Update(store);
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
