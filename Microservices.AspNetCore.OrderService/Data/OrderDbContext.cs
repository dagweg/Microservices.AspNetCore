using Microservices.AspNetCore.OrderService.Models;
using Microsoft.EntityFrameworkCore;

namespace Microservices.AspNetCore.OrderService.Data;

public class OrderDbContext : DbContext
{
  public OrderDbContext(DbContextOptions<OrderDbContext> options)
    : base(options) { }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    base.OnConfiguring(optionsBuilder);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
  }

  public DbSet<Order> Orders { get; set; } = null!;
}
