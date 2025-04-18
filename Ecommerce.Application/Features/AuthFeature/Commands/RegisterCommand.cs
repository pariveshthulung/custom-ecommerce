namespace Ecommerce.Application.Features.AuthFeature.Commands;

public static class RegisterCommand
{
    #region Command
    public record Command : ICommand<BaseResult<Response>>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNo { get; set; } = default!;
        public string Password { get; set; } = default!;
        public int RoleId { get; set; }
    }
    #endregion

    #region Validation
    public class RegisterCommandValidator : AbstractValidator<Command>
    {
        public RegisterCommandValidator(
            IAppUserReadonlyRepository appUserReadonlyRepository,
            ICurrentUserService userService
        )
        {
            RuleFor(x => x.RoleId)
                .NotEmpty()
                .WithMessage("Role is required")
                .GreaterThan(0)
                .WithMessage("Invalid role");
            RuleFor(x => x)
                .Must(
                    (x, cancellationToken) =>
                    {
                        int? currentUserRoleId =
                            userService.UserRole != null
                                ? Enumeration.FromName<RoleEnum>(userService.UserRole).Id
                                : null;
                        return _CheckRegisterPermission(currentUserRoleId, x.RoleId);
                    }
                )
                .WithMessage("You don't have permission to add new user.")
                .When(x => x.RoleId > 0);
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Invalid first name.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Invalid last name.");
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                .WithMessage("Invalid email format.");
            RuleFor(x => x.Email)
                .MustAsync(
                    async (email, cancellationToken) =>
                    {
                        return !await appUserReadonlyRepository.ExistAsync(
                            email,
                            cancellationToken
                        );
                    }
                )
                .WithMessage("Email already exist.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");

            RuleFor(x => x)
                .Must(
                    (x, cancellationToken) =>
                    {
                        var role = Enumeration.FromValue<RoleEnum>(x.RoleId);
                        return role is not null;
                    }
                )
                .WithMessage("Role does not exist");
            RuleFor(x => x.PhoneNo)
                .NotEmpty()
                .WithMessage("Phone number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("Invalid phone number format.");
        }

        private static bool _CheckRegisterPermission(int? currentUserRole, int newUserRole)
        {
            if (currentUserRole is null && newUserRole == RoleEnum.Sales.Id)
                return true;
            if (currentUserRole == RoleEnum.SuperAdmin.Id)
                return true;
            if (currentUserRole == RoleEnum.Admin.Id && newUserRole != RoleEnum.SuperAdmin.Id)
                return true;
            return false;
        }
    }
    #endregion

    #region  Handler
    public class RegisterCommandHandler(
        ILogger<RegisterCommandHandler> logger,
        ICurrentUserService currentUserService,
        UserManager<AppUser> userManager
    ) : ICommandHandler<Command, BaseResult<Response>>
    {
        public async Task<BaseResult<Response>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var currentUser = await currentUserService.GetCurrentUserAsync();
                var appUser = AppUser.Create(
                    request.FirstName,
                    request.LastName,
                    request.Email,
                    request.PhoneNo,
                    request.RoleId,
                    currentUser?.Id,
                    currentUser?.StoreId
                );

                var validationResult = await userManager.CreateAsync(appUser, request.Password);

                if (!validationResult.Succeeded)
                {
                    var errors = validationResult
                        .Errors.Select(e => new Error(400, e.Code, e.Description))
                        .ToList();
                    return BaseResult<Response>.Failure(errors);
                }

                return BaseResult<Response>.Ok(new Response(appUser.UserGuid));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error registering user");
                throw;
            }
        }
    }
    #endregion

    #region Response
    public record Response(Guid UserGuid);
    #endregion
}
