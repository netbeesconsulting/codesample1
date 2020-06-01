using Microsoft.EntityFrameworkCore.Migrations;

namespace Stocky.Core.Migrations
{
    public partial class Adding_CustomerRefToOrderPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                schema: "Application",
                table: "Payment",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                schema: "Application",
                table: "Order",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Payment_CustomerId",
                schema: "Application",
                table: "Payment",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                schema: "Application",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer_CustomerId",
                schema: "Application",
                table: "Order",
                column: "CustomerId",
                principalSchema: "Application",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Customer_CustomerId",
                schema: "Application",
                table: "Payment",
                column: "CustomerId",
                principalSchema: "Application",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_CustomerId",
                schema: "Application",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Customer_CustomerId",
                schema: "Application",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_CustomerId",
                schema: "Application",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Order_CustomerId",
                schema: "Application",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "Application",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "Application",
                table: "Order");
        }
    }
}
