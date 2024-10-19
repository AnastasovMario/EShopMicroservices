namespace Ordering.Domain.ValueObjects
{
  public record OrderName
  {
    private const int DefaultLength = 5;
    public string Value { get; }

    private OrderName(string value) => Value = value;

    //This provides clear and domain specific way to create customer Id instances
    public static OrderName Of(string value)
    {
      ArgumentException.ThrowIfNullOrEmpty(value);
      ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength);

      return new OrderName(value);
    }
  }
}
