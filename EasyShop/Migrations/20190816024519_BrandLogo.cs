using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyShop.Migrations
{
    public partial class BrandLogo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BrandLogo",
                table: "Brands",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrandLogo",
                table: "Brands");
        }
    }
}
