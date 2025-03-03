namespace Domain.Common.ValueObjects;

public class Price
{
  public decimal Amount { get; private set; }
  public string Currency { get; private set; }

  private Price(decimal amount, string currency)
  {
    Amount = amount;
    Currency = currency;
  }

  public static Price Create(decimal amount, string currency)
  {
    if (amount < 0)
    {
      throw new ArgumentOutOfRangeException(nameof(amount), "Price amount cannot be negative.");
    }
    if (string.IsNullOrWhiteSpace(currency))
    {
      throw new ArgumentException("Currency is required for price.", nameof(currency));
    }

    return new Price(amount, currency);
  }

  protected virtual bool Equals(Price other)
  {
    if (other == null)
      return false;
    return Amount == other.Amount && Currency == other.Currency;
  }

  public override bool Equals(object? obj)
  {
    if (ReferenceEquals(null, obj))
      return false;
    if (ReferenceEquals(this, obj))
      return true;
    if (obj.GetType() != this.GetType())
      return false;
    return Equals((Price)obj);
  }

  public override int GetHashCode()
  {
    return HashCode.Combine(Amount, Currency);
  }
}
