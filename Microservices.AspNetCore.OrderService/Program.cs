using Confluent.Kafka;
using Microservices.AspNetCore.Infra;
using Microservices.AspNetCore.Infra.Kafka;
using Microservices.AspNetCore.OrderService.Data;
using Microservices.AspNetCore.OrderService.Models;
using Microservices.AspNetCore.OrderService.Services.EventHandlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(o =>
{
  o.AddDefaultPolicy(p =>
  {
    p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
  });
});

builder.Services.AddSingleton(typeof(IProducer<,>), typeof(ProducerBuilder<,>));
builder.Services.AddSingleton<IKafkaProducer>(sp =>
{
  return new KafkaProducer(
    Options.Create<KafkaOptions>(new KafkaOptions { BootstrapServers = "localhost:9092" })
  );
});

builder.Services.AddDbContext<OrderDbContext>(o =>
  o.UseNpgsql("Host=localhost;Database=orderdb;Username=postgres;Password=123;Port=5432")
);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddEventHandlers();

var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.MapPost(
    "/orders",
    async (
      [FromBody] CreateOrderDto createOrderDto,
      OrderDbContext dbContext,
      IKafkaProducer kafkaProducer
    ) =>
    {
      var order = new Order
      {
        ProductId = createOrderDto.ProductId,
        Quantity = createOrderDto.Quantity,
        Status = OrderStatus.Pending,
      };

      await dbContext.AddAsync(order);
      await dbContext.SaveChangesAsync();

      await kafkaProducer.ProduceAsync<IntegrationEvents.OrderPlacedEvent>(
        KafkaTopics.OrderPlaced,
        new Message<string, IntegrationEvents.OrderPlacedEvent>
        {
          Key = KafkaTopics.OrderPlaced,
          Value = new IntegrationEvents.OrderPlacedEvent(order.Id, order.ProductId, order.Quantity),
        }
      );

      return Results.Accepted(
        $"/orders/{order.Id}",
        new { orderId = order.Id, message = "Order creation request accepted, processing order..." }
      );
    }
  )
  .WithDisplayName("GetOrders");

app.Run();

record CreateOrderDto
{
  public int ProductId { get; init; }
  public int Quantity { get; init; }
}

public static class Extensions
{
  public static IServiceCollection AddEventHandlers(this IServiceCollection services)
  {
    services.AddHostedService<OrderCreatedEventHandler>();
    services.AddHostedService<OrderRejectedEventHandler>();

    return services;
  }
}
