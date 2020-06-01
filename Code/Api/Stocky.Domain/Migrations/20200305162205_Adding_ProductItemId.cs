using Microsoft.EntityFrameworkCore.Migrations;

namespace Stocky.Core.Migrations
{
    public partial class Adding_ProductItemId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProductItemId",
                schema: "Application",
                table: "OrderItem",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductItemId",
                schema: "Application",
                table: "OrderItem",
                column: "ProductItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_ProductList_ProductItemId",
                schema: "Application",
                table: "OrderItem",
                column: "ProductItemId",
                principalSchema: "Application",
                principalTable: "ProductList",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_ProductList_ProductItemId",
                schema: "Application",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_ProductItemId",
                schema: "Application",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "ProductItemId",
                schema: "Application",
                table: "OrderItem");
        }
    }
}
