using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stocky.Core.Migrations
{
    public partial class UpdateProductAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Identity",
                table: "User",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Application",
                table: "ProductList",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Application",
                table: "Product",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                schema: "Application",
                table: "Product",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Application",
                table: "Product",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                schema: "Application",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedUserId",
                schema: "Application",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Application",
                table: "Category",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Application",
                table: "ProductList");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Application",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                schema: "Application",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Application",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                schema: "Application",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ModifiedUserId",
                schema: "Application",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Application",
                table: "Category");
        }
    }
}
