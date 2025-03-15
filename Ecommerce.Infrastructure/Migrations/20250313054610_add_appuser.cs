using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_appuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Customers_UserId",
                schema: "ecom",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                schema: "ecom",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Administrators",
                schema: "ecom");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "ecom");

            migrationBuilder.DropTable(
                name: "UserTypeEnums",
                schema: "ecom");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                schema: "ecom",
                table: "Orders",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                schema: "ecom",
                table: "Orders",
                newName: "IX_Orders_AppUserId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "ecom",
                table: "Carts",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_UserId",
                schema: "ecom",
                table: "Carts",
                newName: "IX_Carts_AppUserId");

            migrationBuilder.CreateTable(
                name: "RoleEnums",
                schema: "ecom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleEnums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                schema: "ecom",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPasswordExpire = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    StoreId = table.Column<long>(type: "bigint", nullable: true),
                    Customer_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Customer_AddressLine = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Customer_StreetNo = table.Column<int>(type: "int", nullable: false),
                    Customer_Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Customer_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Customer_IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUsers_RoleEnums_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "ecom",
                        principalTable: "RoleEnums",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppUsers_Stores_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "ecom",
                        principalTable: "Stores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_RoleId",
                schema: "ecom",
                table: "AppUsers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_StoreId",
                schema: "ecom",
                table: "AppUsers",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_AppUsers_AppUserId",
                schema: "ecom",
                table: "Carts",
                column: "AppUserId",
                principalSchema: "ecom",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AppUsers_AppUserId",
                schema: "ecom",
                table: "Orders",
                column: "AppUserId",
                principalSchema: "ecom",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_AppUsers_AppUserId",
                schema: "ecom",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AppUsers_AppUserId",
                schema: "ecom",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "AppUsers",
                schema: "ecom");

            migrationBuilder.DropTable(
                name: "RoleEnums",
                schema: "ecom");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                schema: "ecom",
                table: "Orders",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_AppUserId",
                schema: "ecom",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                schema: "ecom",
                table: "Carts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_AppUserId",
                schema: "ecom",
                table: "Carts",
                newName: "IX_Carts_UserId");

            migrationBuilder.CreateTable(
                name: "Administrators",
                schema: "ecom",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsPasswordExpire = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreId = table.Column<long>(type: "bigint", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Administrators_Stores_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "ecom",
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTypeEnums",
                schema: "ecom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypeEnums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "ecom",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsPasswordExpire = table.Column<bool>(type: "bit", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserTypeId = table.Column<int>(type: "int", nullable: false),
                    Customer_AddressLine = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Customer_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Customer_IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    Customer_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Customer_Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Customer_StreetNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_UserTypeEnums_UserTypeId",
                        column: x => x.UserTypeId,
                        principalSchema: "ecom",
                        principalTable: "UserTypeEnums",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administrators_StoreId",
                schema: "ecom",
                table: "Administrators",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserTypeId",
                schema: "ecom",
                table: "Customers",
                column: "UserTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Customers_UserId",
                schema: "ecom",
                table: "Carts",
                column: "UserId",
                principalSchema: "ecom",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                schema: "ecom",
                table: "Orders",
                column: "CustomerId",
                principalSchema: "ecom",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
