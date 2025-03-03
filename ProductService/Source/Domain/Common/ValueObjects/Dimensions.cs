namespace Domain.Common.ValueObjects;

public class Dimensions
{
  public decimal Height { get; private set; }
  public decimal Width { get; private set; }
  public decimal Depth { get; private set; }
  public string Unit { get; private set; }

  private Dimensions(decimal height, decimal width, decimal depth, string unit)
  {
    Height = height;
    Width = width;
    Depth = depth;
    Unit = unit;
  }

  public static Dimensions Create(decimal height, decimal width, decimal depth, string unit)
  {
    if (height < 0 || width < 0 || depth < 0)
    {
      throw new ArgumentOutOfRangeException("Dimensions cannot be negative.");
    }
    if (string.IsNullOrWhiteSpace(unit))
    {
      throw new ArgumentException("Unit for dimensions is required.", nameof(unit));
    }
    return new Dimensions(height, width, depth, unit);
  }

  protected virtual bool Equals(Dimensions other)
  {
    if (other == null)
      return false;
    return Height == other.Height
      && Width == other.Width
      && Depth == other.Depth
      && Unit == other.Unit;
  }

  public override bool Equals(object? obj)
  {
    if (ReferenceEquals(null, obj))
      return false;
    if (ReferenceEquals(this, obj))
      return true;
    if (obj.GetType() != this.GetType())
      return false;
    return Equals((Dimensions)obj);
  }

  public override int GetHashCode()
  {
    return HashCode.Combine(Height, Width, Depth, Unit);
  }
}
