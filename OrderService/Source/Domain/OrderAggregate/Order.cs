using Domain.Common.Models;
using Domain.OrderAggregate.Events;
using Domain.OrderAggregate.Interfaces;
using OrderService.Domain.Common.ValueObjects;
using OrderService.Domain.OrderAggregate.Enums;
using OrderService.Domain.OrderAggregate.ValueObjects;

public class Order : AggregateRoot
{
  public Guid OrderId { get; private set; }

  public DateTime OrderDate { get; private set; }
  public Guid CustomerId { get; private set; }
  public OrderStatus Status { get; private set; }
  public Address ShippingAddress { get; private set; }

  private readonly List<OrderItem> _orderItems;
  public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

  private Order(Guid orderId, Guid customerId, Address shippingAddress)
  {
    OrderId = orderId;
    OrderDate = DateTime.UtcNow;
    CustomerId = customerId;
    Status = OrderStatus.Draft;
    ShippingAddress = shippingAddress;
    _orderItems = new List<OrderItem>();
  }

  public static Order CreateOrder(Guid customerId, Address shippingAddress)
  {
    if (shippingAddress == null)
    {
      throw new ArgumentNullException(nameof(shippingAddress), "Shipping address is required.");
    }

    int newOrderId = GenerateNewOrderId();

    var order = new Order(Guid.NewGuid(), customerId, shippingAddress);

    return order;
  }

  private static int GenerateNewOrderId()
  {
    return new Random().Next(1000, 9999);
  }

  public void AddOrderItem(Guid productId, int quantity)
  {
    EnsureOrderIsDraft();

    var existingItem = _orderItems.FirstOrDefault(item => item.ProductId == productId);
    if (existingItem != null)
    {
      ChangeOrderItemQuantity(productId, existingItem.Quantity + quantity);
    }
    else
    {
      var newItem = OrderItem.Create(productId, quantity);
      _orderItems.Add(newItem);
    }
  }

  public void RemoveOrderItem(Guid productId)
  {
    EnsureOrderIsDraft();

    var itemToRemove = _orderItems.FirstOrDefault(item => item.ProductId == productId);
    if (itemToRemove != null)
    {
      _orderItems.Remove(itemToRemove);
    }
    else
    {
      throw new InvalidOperationException(
        $"OrderItem with ProductId '{productId}' not found in the order."
      );
    }
  }

  public void ChangeOrderItemQuantity(Guid productId, int newQuantity)
  {
    EnsureOrderIsDraft();

    if (newQuantity <= 0)
    {
      throw new ArgumentOutOfRangeException(
        nameof(newQuantity),
        "Quantity must be a positive value."
      );
    }

    var itemToUpdate = _orderItems.FirstOrDefault(item => item.ProductId == productId);
    if (itemToUpdate != null)
    {
      _orderItems.Remove(itemToUpdate);
      _orderItems.Add(OrderItem.Create(itemToUpdate.ProductId, newQuantity));
    }
    else
    {
      throw new InvalidOperationException(
        $"OrderItem with ProductId '{productId}' not found in the order."
      );
    }
  }

  public void SetShippingAddress(Address newShippingAddress)
  {
    EnsureOrderIsDraft();

    if (newShippingAddress == null)
    {
      throw new ArgumentNullException(
        nameof(newShippingAddress),
        "New shipping address cannot be null."
      );
    }
    ShippingAddress = newShippingAddress;
  }

  public void SubmitOrder()
  {
    EnsureOrderIsDraft();

    if (!_orderItems.Any())
    {
      throw new InvalidOperationException("Cannot submit an order with no items.");
    }

    Status = OrderStatus.Submitted;

    RaiseDomainEvent(new OrderPlacedEvent(this));
  }

  public void CancelOrder()
  {
    if (Status != OrderStatus.Draft && Status != OrderStatus.Submitted)
    {
      throw new InvalidOperationException($"Order cannot be cancelled in '{Status}' state.");
    }

    Status = OrderStatus.Cancelled;

    RaiseDomainEvent(new OrderCancelledEvent(this));
  }

  private void EnsureOrderIsDraft()
  {
    if (Status != OrderStatus.Draft)
    {
      throw new InvalidOperationException(
        $"Order is in '{Status}' state and cannot be modified further."
      );
    }
  }

  private static void RaiseDomainEvent(IOrderDomainEvent domainEvent)
  {
    Console.WriteLine(
      $"Domain Event Raised: {domainEvent.GetType().Name} for OrderId: {domainEvent.Order.OrderId}"
    );
  }
}
