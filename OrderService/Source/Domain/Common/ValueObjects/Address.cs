namespace OrderService.Domain.Common.ValueObjects;

public class Address
{
  public string Street { get; private set; }
  public string City { get; private set; }
  public string State { get; private set; }
  public string ZipCode { get; private set; }
  public string Country { get; private set; }

  private Address(string street, string city, string state, string zipCode, string country)
  {
    Street = street;
    City = city;
    State = state;
    ZipCode = zipCode;
    Country = country;
  }

  public static Address Create(
    string street,
    string city,
    string state,
    string zipCode,
    string country
  )
  {
    if (string.IsNullOrWhiteSpace(street))
    {
      throw new ArgumentException("Street address is required.", nameof(street));
    }
    if (string.IsNullOrWhiteSpace(city))
    {
      throw new ArgumentException("City is required.", nameof(city));
    }
    if (string.IsNullOrWhiteSpace(state))
    {
      throw new ArgumentException("State is required.", nameof(state));
    }
    if (string.IsNullOrWhiteSpace(zipCode))
    {
      throw new ArgumentException("Zip code is required.", nameof(zipCode));
    }
    if (string.IsNullOrWhiteSpace(country))
    {
      throw new ArgumentException("Country is required.", nameof(country));
    }

    return new Address(street, city, state, zipCode, country);
  }

  protected virtual bool Equals(Address other)
  {
    if (other == null)
      return false;
    return Street == other.Street
      && City == other.City
      && State == other.State
      && ZipCode == other.ZipCode
      && Country == other.Country;
  }

  public override bool Equals(object? obj)
  {
    if (ReferenceEquals(null, obj))
      return false;
    if (ReferenceEquals(this, obj))
      return true;
    if (obj.GetType() != this.GetType())
      return false;
    return Equals((Address)obj);
  }

  public override int GetHashCode()
  {
    return HashCode.Combine(Street, City, State, ZipCode, Country);
  }
}
