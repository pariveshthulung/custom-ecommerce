using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Infrastructure.Repository;

public class SeederRepository(IDbConnection dbConnection, PasswordHasher<AppUser> passwordHasher)
    : ISeederRepository
{
    public async Task<int> SeedAdministrator()
    {
        try
        {
            string sql = "SELECT COUNT(1) FROM [EcommerceDb].[ecom].[AppUsers]";

            int count = await dbConnection.ExecuteScalarAsync<int>(sql);
            bool exists = count > 0;
            if (!exists)
            {
                var appuser = AppUser.Create(
                    "Super",
                    "Admin",
                    "superadmin@gmail.com",
                    "1234567890",
                    2
                );
                var password = passwordHasher.HashPassword(appuser, "superadmin");
                var query =
                    $@"INSERT INTO [EcommerceDb].[ecom].[AppUsers] (
                [FirstName], [LastName], [PhoneNo], [RefreshToken], [RefreshTokenExpiryTime], 
                [IsPasswordExpire], [IsDeleted], [RoleId], [IsActive], [StoreId], 
                [Customer_City], [Customer_AddressLine], [Customer_StreetNo], [Customer_Region], 
                [Customer_PostalCode], [Customer_IsDefault], [UserName], [NormalizedUserName], 
                [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], 
                [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], 
                [LockoutEnd], [LockoutEnabled], [AccessFailedCount]
                ) 
                VALUES (
                    'Super', 'Admin', '1234567890', 'sample-refresh-token', '2025-12-31 23:59:59', 
                    0, 0, 2, 1, Null, 
                    'New York', '123 Main St', '456', 'NY', 
                    '10001', 1, 'johndoe', 'JOHNDOE', 
                    'superadmin@gmail.com', 'SUPERADMIN@GMAIL.COM', 1, '{password}', 'security-stamp', 
                    'concurrency-stamp', '9876543210', 1, 0, 
                    NULL, 1, 0
                )";

                return await dbConnection.ExecuteAsync(query);
            }
            return 1;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<int> SeedRole()
    {
        try
        {
            string sql = "SELECT COUNT(1) FROM [EcommerceDb].[ecom].[RoleEnums]";

            int count = await dbConnection.ExecuteScalarAsync<int>(sql);
            bool exists = count > 0;
            if (!exists)
            {
                var role = Enumeration.GetAll<RoleEnum>();
                var query = "INSERT INTO ecom.RoleEnums (Id, Name) VALUES (@Id,@Name);";

                return await dbConnection.ExecuteAsync(query, role);
            }
            return 1;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
