namespace Ecommerce.Shared.DomainDesign.Extentions;

public static class FluentValidationExtension
{
    public sealed class ValidationExceptionException(ValidationFailure validationFailure)
        : ValidationException([validationFailure]);
}
