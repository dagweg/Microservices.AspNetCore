using Core.Command;
using Infra;
using OrderService.Domain.Common.ValueObjects;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi().AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost(
  "/order",
  async () =>
  {
    await new CreateOrderCommandHandler().Handle(
      new CreateOrderCommand
      {
        CustomerId = Guid.NewGuid(),
        ShippingAddress = Address.Create("street", "city", "state", "zipCode", "country"),
        OrderItems = new List<CreateOrderCommand.OrderItemCommand>
        {
          new CreateOrderCommand.OrderItemCommand { ProductId = Guid.NewGuid(), Quantity = 1 },
          new CreateOrderCommand.OrderItemCommand { ProductId = Guid.NewGuid(), Quantity = 2 },
        },
      },
      default
    );
  }
);

app.Run();
