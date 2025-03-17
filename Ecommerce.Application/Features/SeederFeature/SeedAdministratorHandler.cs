namespace Ecommerce.Application.Features.SeederFeature;

public static class SeedAdministrator
{
    public record Command() : ICommand<BaseResult<Response>>;

    public record Response(int Id);

    public class SeedAdministratorHandler(ISeederRepository seederRepository)
        : ICommandHandler<Command, BaseResult<Response>>
    {
        public async Task<BaseResult<Response>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var response = await seederRepository.SeedAdministrator();
                return BaseResult<Response>.Ok(new Response(response));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
