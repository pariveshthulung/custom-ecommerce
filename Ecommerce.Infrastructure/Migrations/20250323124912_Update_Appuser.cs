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
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Stores_StoreId",
                schema: "ecom",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_StoreId",
                schema: "ecom",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "StoreId",
                schema: "ecom",
                table: "AppUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "StoreGuid",
                schema: "ecom",
                table: "AppUsers",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoreGuid",
                schema: "ecom",
                table: "AppUsers");

            migrationBuilder.AddColumn<long>(
                name: "StoreId",
                schema: "ecom",
                table: "AppUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_StoreId",
                schema: "ecom",
                table: "AppUsers",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Stores_StoreId",
                schema: "ecom",
                table: "AppUsers",
                column: "StoreId",
                principalSchema: "ecom",
                principalTable: "Stores",
                principalColumn: "Id");
        }
    }
}
