using Ecommerce.Shared.Wrappers;

namespace Ecommerce.Application.Features.AuthFeature.Commands;

public record LoginCommand : ICommand<BaseResult<LoginResponse>>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
            .WithMessage("Invalid email format.");
    }
}
