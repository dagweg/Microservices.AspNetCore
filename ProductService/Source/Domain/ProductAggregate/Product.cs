namespace Domain.ProductAggregate;

using Domain.Common.ValueObjects;
using Domain.ProductAggregate.Events;
using Domain.ProductAggregate.Interfaces;

public class Product
{
  public Guid ProductId { get; private set; }
  public string ProductCode { get; private set; }
  public string Name { get; private set; }
  public string Description { get; private set; }
  public Price CurrentPrice { get; private set; }
  public Dimensions ProductDimensions { get; private set; }
  public int StockQuantity { get; private set; }
  public bool IsActive { get; private set; }

  private readonly List<string> _categories;
  public IReadOnlyCollection<string> Categories => _categories.AsReadOnly();

  private Product(
    Guid productId,
    string productCode,
    string name,
    string description,
    Price price,
    Dimensions dimensions,
    int stockQuantity
  )
  {
    ProductId = productId;
    ProductCode = productCode;
    Name = name;
    Description = description;
    CurrentPrice = price;
    ProductDimensions = dimensions;
    StockQuantity = stockQuantity;
    IsActive = true;
    _categories = new List<string>();
  }

  public static Product CreateProduct(
    string productCode,
    string name,
    string description,
    Price price,
    Dimensions dimensions,
    int stockQuantity
  )
  {
    if (string.IsNullOrWhiteSpace(productCode))
    {
      throw new ArgumentException("Product code is required.", nameof(productCode));
    }
    if (string.IsNullOrWhiteSpace(name))
    {
      throw new ArgumentException("Product name is required.", nameof(name));
    }
    if (description == null)
    {
      description = string.Empty;
    }
    if (price == null)
    {
      throw new ArgumentNullException(nameof(price), "Price is required.");
    }
    if (dimensions == null)
    {
      throw new ArgumentNullException(nameof(dimensions), "Dimensions are required.");
    }

    return new Product(
      Guid.NewGuid(),
      productCode,
      name,
      description,
      price,
      dimensions,
      stockQuantity
    );
  }

  public void UpdateProductDetails(string name, string description, Dimensions newDimensions)
  {
    if (string.IsNullOrWhiteSpace(name))
    {
      throw new ArgumentException("Product name cannot be empty.", nameof(name));
    }
    if (newDimensions == null)
    {
      throw new ArgumentNullException(nameof(newDimensions), "Dimensions cannot be null.");
    }

    Name = name;
    Description = description;
    ProductDimensions = newDimensions;
  }

  public void ChangePrice(Price newPrice)
  {
    if (newPrice == null)
    {
      throw new ArgumentNullException(nameof(newPrice), "New price cannot be null.");
    }
    if (newPrice.Amount <= 0)
    {
      throw new ArgumentOutOfRangeException(
        nameof(newPrice.Amount),
        "Price amount must be positive."
      );
    }

    CurrentPrice = newPrice;

    RaiseDomainEvent(new ProductPriceChangedEvent(ProductId, newPrice));
  }

  public void AddCategory(string categoryName)
  {
    if (string.IsNullOrWhiteSpace(categoryName))
    {
      throw new ArgumentException("Category name cannot be empty.", nameof(categoryName));
    }
    if (!_categories.Contains(categoryName, StringComparer.OrdinalIgnoreCase))
    {
      _categories.Add(categoryName.Trim());
    }
  }

  public void RemoveCategory(string categoryName)
  {
    if (string.IsNullOrWhiteSpace(categoryName))
    {
      throw new ArgumentException("Category name cannot be empty.", nameof(categoryName));
    }
    if (_categories.Contains(categoryName, StringComparer.OrdinalIgnoreCase))
    {
      _categories.Remove(categoryName);
    }
  }

  public void IncreaseStock(int quantityToAdd)
  {
    if (quantityToAdd <= 0)
    {
      throw new ArgumentOutOfRangeException(
        nameof(quantityToAdd),
        "Quantity to add must be positive."
      );
    }
    StockQuantity += quantityToAdd;
  }

  public void DecreaseStock(int quantityToRemove)
  {
    if (quantityToRemove <= 0)
    {
      throw new ArgumentOutOfRangeException(
        nameof(quantityToRemove),
        "Quantity to remove must be positive."
      );
    }
    if (StockQuantity < quantityToRemove)
    {
      throw new InvalidOperationException(
        $"Not enough stock to remove {quantityToRemove} units. Current stock: {StockQuantity}."
      );
    }
    StockQuantity -= quantityToRemove;

    if (StockQuantity == 0)
    {
      RaiseDomainEvent(new ProductOutOfStockEvent(ProductId));
    }
  }

  public void MarkAsInactive()
  {
    if (IsActive)
    {
      IsActive = false;

      RaiseDomainEvent(new ProductMarkedInactiveEvent(ProductId));
    }
  }

  public void MarkAsActive()
  {
    if (!IsActive)
    {
      IsActive = true;

      RaiseDomainEvent(new ProductMarkedActiveEvent(ProductId));
    }
  }

  private static void RaiseDomainEvent(IProductDomainEvent domainEvent)
  {
    Console.WriteLine(
      $"Product Domain Event Raised: {domainEvent.GetType().Name} for ProductId: {domainEvent.ProductId}"
    );
  }
}
