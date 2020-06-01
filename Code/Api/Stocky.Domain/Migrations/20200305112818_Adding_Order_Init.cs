using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stocky.Core.Migrations
{
    public partial class Adding_Order_Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Application",
                table: "Customer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Contact",
                schema: "Application",
                table: "Customer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Company",
                schema: "Application",
                table: "Customer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatorUserId = table.Column<long>(nullable: false),
                    ModifiedUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    OrderStatus = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatorUserId = table.Column<long>(nullable: false),
                    ModifiedUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    DiscountType = table.Column<byte>(nullable: false),
                    DiscountTypeValue = table.Column<string>(nullable: true),
                    Discount = table.Column<decimal>(nullable: false),
                    TotalPayable = table.Column<decimal>(nullable: false),
                    OrderId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "Application",
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Application",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatorUserId = table.Column<long>(nullable: false),
                    ModifiedUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OrderId = table.Column<long>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    PaymentStatus = table.Column<byte>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    TotalPayable = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "Application",
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartialPayment",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatorUserId = table.Column<long>(nullable: false),
                    ModifiedUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PaymentId = table.Column<long>(nullable: false),
                    PaidDateTime = table.Column<DateTime>(nullable: false),
                    PaidAmount = table.Column<decimal>(nullable: false),
                    PaymentMethod = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartialPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartialPayment_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalSchema: "Application",
                        principalTable: "Payment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChequePayment",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatorUserId = table.Column<long>(nullable: false),
                    ModifiedUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PartialPaymentId = table.Column<long>(nullable: false),
                    ChequeDate = table.Column<DateTime>(nullable: false),
                    Bank = table.Column<string>(nullable: true),
                    Branch = table.Column<string>(nullable: true),
                    ChequeNumber = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    ChequeStatus = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChequePayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChequePayment_PartialPayment_PartialPaymentId",
                        column: x => x.PartialPaymentId,
                        principalSchema: "Application",
                        principalTable: "PartialPayment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChequePayment_PartialPaymentId",
                schema: "Application",
                table: "ChequePayment",
                column: "PartialPaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                schema: "Application",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductId",
                schema: "Application",
                table: "OrderItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PartialPayment_PaymentId",
                schema: "Application",
                table: "PartialPayment",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_OrderId",
                schema: "Application",
                table: "Payment",
                column: "OrderId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChequePayment",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "OrderItem",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "PartialPayment",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "Payment",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "Application");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Application",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Contact",
                schema: "Application",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Company",
                schema: "Application",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
