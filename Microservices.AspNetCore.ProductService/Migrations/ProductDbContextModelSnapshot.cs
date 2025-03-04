﻿// <auto-generated />
using Microservices.AspNetCore.ProductService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Microservices.AspNetCore.ProductService.Migrations
{
    [DbContext(typeof(ProductDbContext))]
    partial class ProductDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microservices.AspNetCore.ProductService.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("Stock")
                        .HasColumnType("integer");

                    b.Property<string>("ThumbUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Acer Aspire 5 Slim Laptop",
                            Price = 349.99m,
                            Stock = 100,
                            ThumbUrl = "https://c1.neweggimages.com/productimage/nb640/A8X5S210204h0KxK.jpg"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Apple MacBook Air",
                            Price = 999.99m,
                            Stock = 50,
                            ThumbUrl = "https://wapcomputer.com/wp-content/uploads/2020/12/images-1-19.jpeg"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Dell XPS 13",
                            Price = 1199.99m,
                            Stock = 25,
                            ThumbUrl = "https://cdn11.bigcommerce.com/s-o9pppsyjzh/images/stencil/1280x1280/products/471705/12169085/N5302460__1__06999.1690009763.jpg?c=1"
                        },
                        new
                        {
                            Id = 4,
                            Name = "HP Pavilion 15",
                            Price = 549.99m,
                            Stock = 75,
                            ThumbUrl = "https://hk-media.apjonlinecdn.com/catalog/product/cache/b3b166914d87ce343d4dc5ec5117b502/c/0/c07961278_1_2.png"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
