namespace Ordering.Domain.ValueObjects
{
  public record ProductId
  {
    public Guid Value { get; }

    private ProductId(Guid value) => Value = value;

    //This provides clear and domain specific way to create ProductId Id instances
    public static ProductId Of(Guid value)
    {
      //Domain validation
      ArgumentNullException.ThrowIfNull(value);
      if (value == Guid.Empty)
      {
        throw new DomainException("ProductId cannot be empty.");
      }

      return new ProductId(value);
    }

  }
}
