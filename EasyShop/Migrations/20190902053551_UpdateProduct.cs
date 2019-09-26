using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyShop.Migrations
{
    public partial class UpdateProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sale",
                table: "Products",
                newName: "Discount");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ProductFeatures",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FeatureName",
                table: "ProductFeatures",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ParentCategoryName",
                table: "ParentCategories",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BrandName",
                table: "Brands",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "Products",
                newName: "Sale");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ProductFeatures",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FeatureName",
                table: "ProductFeatures",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ParentCategoryName",
                table: "ParentCategories",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "BrandName",
                table: "Brands",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
