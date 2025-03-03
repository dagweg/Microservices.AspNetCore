using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var productService = builder.AddProject<Microservices_AspNetCore_ProductService>(
  "api-service-product"
);
var orderService = builder.AddProject<Microservices_AspNetCore_OrderService>("api-service-order");

builder.Build().Run();
