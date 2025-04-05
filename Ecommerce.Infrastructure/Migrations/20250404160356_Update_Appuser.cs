using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_Appuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductsId",
                schema: "ecom",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "StoreGuid",
                schema: "ecom",
                table: "AppUsers");

            migrationBuilder.AddColumn<long>(
                name: "StoreId",
                schema: "ecom",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "StoreId",
                schema: "ecom",
                table: "EventLogs",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StoreId",
                schema: "ecom",
                table: "AppUsers",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoreId",
                schema: "ecom",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StoreId",
                schema: "ecom",
                table: "EventLogs");

            migrationBuilder.DropColumn(
                name: "StoreId",
                schema: "ecom",
                table: "AppUsers");

            migrationBuilder.AddColumn<string>(
                name: "ProductsId",
                schema: "ecom",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "StoreGuid",
                schema: "ecom",
                table: "AppUsers",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
