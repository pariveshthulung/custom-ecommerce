namespace Ecommerce.Application.Features.StoreFeature;

public static class CreateStoreCommand
{
    #region Command
    public record Command(string StoreName) : ICommand<BaseResult<Response>>;
    #endregion
    #region Validation
    public sealed class CreateStoreCommandValidator : AbstractValidator<Command>
    {
        public CreateStoreCommandValidator()
        {
            RuleFor(x => x.StoreName).NotEmpty().WithMessage("Invalid store name");
        }
    }
    #endregion

    #region Handler
    public sealed class Handler : ICommandHandler<Command, BaseResult<Response>>
    {
        public Task<BaseResult<Response>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region Response
    public record Response(Guid StoreGuid);
    #endregion
}
