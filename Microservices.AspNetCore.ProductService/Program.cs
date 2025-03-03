using Microservices.AspNetCore.ProductService.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ProductDbContext>(o =>
  o.UseNpgsql("Host=localhost;Database=productdb;Username=postgres;Password=123;Port=5432")
);

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet(
  "/products",
  async (ProductDbContext dbContext) =>
  {
    return Results.Ok(await dbContext.Products.ToListAsync());
  }
);

app.Run();
