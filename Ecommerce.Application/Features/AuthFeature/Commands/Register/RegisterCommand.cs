using Ecommerce.Shared.Wrappers;

namespace Ecommerce.Application.Features.AuthFeature.Commands.Register;

public record RegisterCommand : ICommand<BaseResult<Response>>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNo { get; set; } = default!;
    public string Password { get; set; } = default!;
    public int RoleId { get; set; }
}

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator(
        IAppUserReadonlyRepository appUserReadonlyRepository,
        ICurrentUserService userService
    )
        : base()
    {
        RuleFor(x => x)
            .Must(
                (x, cancellationToken) =>
                {
                    var currentUserRoleId = Enumeration
                        .FromName<RoleEnum>(userService.UserRole ?? "")
                        .Id;
                    return CheckRegisterPermission(currentUserRoleId, x.RoleId);
                }
            )
            .WithMessage("You don't have permission to add new user.");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("Invalid first name.");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("Invalid last name.");
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
            .WithMessage("Invalid email format.");
        RuleFor(x => x.Email)
            .MustAsync(
                async (email, cancellationToken) =>
                {
                    return !await appUserReadonlyRepository.ExistAsync(email, cancellationToken);
                }
            )
            .WithMessage("Email already exist.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Invalid password");
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

    public bool CheckRegisterPermission(int currentUserRole, int newUserRole)
    {
        if (currentUserRole == RoleEnum.SuperAdmin.Id)
            return true;
        if (currentUserRole == RoleEnum.Admin.Id && newUserRole != RoleEnum.SuperAdmin.Id)
            return true;
        return false;
    }
}
