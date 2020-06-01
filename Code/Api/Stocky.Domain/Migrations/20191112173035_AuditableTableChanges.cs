using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stocky.Core.Migrations
{
    public partial class AuditableTableChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ModifiedUserId",
                schema: "Identity",
                table: "User",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ModifiedUserId",
                schema: "Application",
                table: "ProductList",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Application",
                table: "Category",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                schema: "Application",
                table: "Category",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                schema: "Application",
                table: "Category",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedUserId",
                schema: "Application",
                table: "Category",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Application",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                schema: "Application",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                schema: "Application",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "ModifiedUserId",
                schema: "Application",
                table: "Category");

            migrationBuilder.AlterColumn<long>(
                name: "ModifiedUserId",
                schema: "Identity",
                table: "User",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ModifiedUserId",
                schema: "Application",
                table: "ProductList",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
