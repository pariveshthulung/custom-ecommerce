using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Remodeldomainrelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_AppUsers_AppUserId",
                schema: "ecom",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Products_ProductId",
                schema: "ecom",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AppUsers_AppUserId",
                schema: "ecom",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Stores_StoreId",
                schema: "ecom",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_StoreId",
                schema: "ecom",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AppUserId",
                schema: "ecom",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ProductId",
                schema: "ecom",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Carts_AppUserId",
                schema: "ecom",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "StoreId",
                schema: "ecom",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "ecom",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "ProductsId",
                schema: "ecom",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CategoriesId",
                schema: "ecom",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "CartId",
                schema: "ecom",
                table: "AppUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrdersId",
                schema: "ecom",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_CartId",
                schema: "ecom",
                table: "AppUsers",
                column: "CartId",
                unique: true,
                filter: "[CartId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Carts_CartId",
                schema: "ecom",
                table: "AppUsers",
                column: "CartId",
                principalSchema: "ecom",
                principalTable: "Carts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Carts_CartId",
                schema: "ecom",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_CartId",
                schema: "ecom",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "ProductsId",
                schema: "ecom",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "CategoriesId",
                schema: "ecom",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CartId",
                schema: "ecom",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "OrdersId",
                schema: "ecom",
                table: "AppUsers");

            migrationBuilder.AddColumn<long>(
                name: "StoreId",
                schema: "ecom",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                schema: "ecom",
                table: "Categories",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_StoreId",
                schema: "ecom",
                table: "Products",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AppUserId",
                schema: "ecom",
                table: "Orders",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProductId",
                schema: "ecom",
                table: "Categories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_AppUserId",
                schema: "ecom",
                table: "Carts",
                column: "AppUserId",
                unique: true);

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
                name: "FK_Categories_Products_ProductId",
                schema: "ecom",
                table: "Categories",
                column: "ProductId",
                principalSchema: "ecom",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AppUsers_AppUserId",
                schema: "ecom",
                table: "Orders",
                column: "AppUserId",
                principalSchema: "ecom",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Stores_StoreId",
                schema: "ecom",
                table: "Products",
                column: "StoreId",
                principalSchema: "ecom",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
