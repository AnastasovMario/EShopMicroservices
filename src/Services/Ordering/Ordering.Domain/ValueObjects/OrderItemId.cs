namespace Ordering.Domain.ValueObjects
{
  public record OrderItemId
  {
    public Guid Value { get; }

    private OrderItemId(Guid value) => Value = value;

    //This provides clear and domain specific way to create OrderItemId  instances
    public static OrderItemId Of(Guid value)
    {
      //Domain validation
      ArgumentNullException.ThrowIfNull(value);
      if (value == Guid.Empty)
      {
        throw new DomainException("OrderItemId cannot be empty.");
      }

      return new OrderItemId(value);
    }
  }
}
