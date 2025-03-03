using Microservices.AspNetCore.OrderService.Data;
using Microservices.AspNetCore.OrderService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<OrderDbContext>(o =>
  o.UseNpgsql("Host=localhost;Database=orderdb;Username=postgres;Password=123;Port=5432")
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

app.MapPost(
    "/orders",
    ([FromBody] CreateOrderDto createOrderDto, OrderDbContext dbContext) =>
    {
      var order = new Order
      {
        ProductId = createOrderDto.ProductId,
        Quantity = createOrderDto.Quantity,
      };

      dbContext.SaveChanges();

      return Results.Created($"/orders/{order.Id}", order);
    }
  )
  .WithDisplayName("GetOrders");

app.Run();

record CreateOrderDto
{
  public int ProductId { get; init; }
  public int Quantity { get; init; }
}
