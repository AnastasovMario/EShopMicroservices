namespace Ordering.Domain.ValueObjects
{
  public record CustomerId
  {
    public Guid Value { get; set; }

    private CustomerId(Guid value) => Value = value;


    //This provides clear and domain specific way to create customer Id instances
    public static CustomerId Of(Guid value)
    {
      //Domain validation
      ArgumentNullException.ThrowIfNull(value);
      if (value == Guid.Empty)
      {
        throw new DomainException("CustomerId cannot be empty.");
      }

      return new CustomerId(value);
    }
  }
}
