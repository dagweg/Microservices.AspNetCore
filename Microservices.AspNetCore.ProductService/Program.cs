using Microservices.AspNetCore.Infra.Kafka;
using Microservices.AspNetCore.ProductService.Data;
using Microservices.AspNetCore.ProductService.Services.EventHandlers;
using Microsoft.AspNetCore.Http.HttpResults;
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

builder.Services.AddDbContext<ProductDbContext>(o =>
  o.UseNpgsql("Host=localhost;Database=productdb;Username=postgres;Password=123;Port=5432")
);

builder.Services.AddSingleton<IKafkaProducer>(sp =>
{
  return new KafkaProducer(
    Options.Create<KafkaOptions>(new KafkaOptions { BootstrapServers = "localhost:9092" })
  );
});

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

app.MapGet(
  "/products",
  async (ProductDbContext dbContext) =>
  {
    return Results.Ok(await dbContext.Products.OrderBy(p => p.Name).ToListAsync());
  }
);

app.Run();

public static class Extensions
{
  public static IServiceCollection AddEventHandlers(this IServiceCollection services)
  {
    services.AddHostedService<OrderPlacedEventHandler>();

    return services;
  }
}
