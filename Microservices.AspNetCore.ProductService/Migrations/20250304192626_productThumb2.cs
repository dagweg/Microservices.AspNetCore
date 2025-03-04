using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Microservices.AspNetCore.ProductService.Migrations
{
    /// <inheritdoc />
    public partial class productThumb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "ThumbUrl",
                value: "https://hk-media.apjonlinecdn.com/catalog/product/cache/b3b166914d87ce343d4dc5ec5117b502/c/0/c07961278_1_2.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "ThumbUrl",
                value: "https://www.costco.com/medias/sys_master/images/hb3/hb3/hf2/14407307336798.jpg");
        }
    }
}
