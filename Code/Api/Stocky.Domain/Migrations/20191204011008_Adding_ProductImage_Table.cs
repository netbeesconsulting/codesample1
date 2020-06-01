using Microsoft.EntityFrameworkCore.Migrations;

namespace Stocky.Core.Migrations
{
    public partial class Adding_ProductImage_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                schema: "Application",
                table: "Product");

            migrationBuilder.CreateTable(
                name: "ProductImage",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(nullable: true),
                    ProductId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImage_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Application",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductId",
                schema: "Application",
                table: "ProductImage",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImage",
                schema: "Application");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                schema: "Application",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
