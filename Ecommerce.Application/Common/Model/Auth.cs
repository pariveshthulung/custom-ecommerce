namespace Ecommerce.Application.Common.Model;

public record LoginResponse(string Token, string RefreshToken);

public record RegisterBaseModel(string Name, string Email, string Password, int RoleId);
