using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Microservices.AspNetCore.ProductService.Migrations
{
    /// <inheritdoc />
    public partial class productThumb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbUrl",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ThumbUrl",
                value: "https://c1.neweggimages.com/productimage/nb640/A8X5S210204h0KxK.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "ThumbUrl",
                value: "https://wapcomputer.com/wp-content/uploads/2020/12/images-1-19.jpeg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "ThumbUrl",
                value: "https://cdn11.bigcommerce.com/s-o9pppsyjzh/images/stencil/1280x1280/products/471705/12169085/N5302460__1__06999.1690009763.jpg?c=1");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "ThumbUrl",
                value: "https://www.costco.com/medias/sys_master/images/hb3/hb3/hf2/14407307336798.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbUrl",
                table: "Products");
        }
    }
}
