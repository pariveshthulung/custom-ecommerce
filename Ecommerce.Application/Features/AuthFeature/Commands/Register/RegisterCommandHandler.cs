namespace Ecommerce.Application.Features.AuthFeature.Commands.Register;

public class RegisterCommandHandler(
    // Logger<RegisterCommandHandler> logger,
    IAppUserReadonlyRepository appUserReadonlyRepository,
    UserManager<AppUser> userManager
) : ICommandHandler<RegisterCommand, BaseResult<Response>>
{
    public async Task<BaseResult<Response>> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var emailExist = await appUserReadonlyRepository.ExistAsync(
                request.Email,
                cancellationToken
            );
            var appUser = AppUser.Create(
                request.FirstName,
                request.LastName,
                request.Email,
                request.PhoneNo,
                request.RoleId
            );

            var validationResult = await userManager.CreateAsync(appUser, request.Password);

            if (!validationResult.Succeeded)
            {
                var errors = validationResult
                    .Errors.Select(e => new Error(400, e.Code, e.Description))
                    .ToList();
                return BaseResult<Response>.Failure(errors);
            }

            return BaseResult<Response>.Ok(new Response(appUser.Id));
        }
        catch (Exception ex)
        {
            // logger.LogError(ex, "Error registering user");
            throw;
        }
    }
}

public record Response(long UserId);
