namespace Ordering.Domain.ValueObjects
{
  public record OrderId
  {
    public Guid Value { get; }

    private OrderId(Guid value) => Value = value;

    //This provides clear and domain specific way to create order Id instances
    public static OrderId Of(Guid value)
    {
      //Domain validation
      ArgumentNullException.ThrowIfNull(value);
      if (value == Guid.Empty)
      {
        throw new DomainException("OrderId cannot be empty.");
      }

      return new OrderId(value);
    }
  }
}
