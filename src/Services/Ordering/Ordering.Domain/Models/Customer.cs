namespace Ordering.Domain.Models
{
  public class Customer : Entity<CustomerId>
  {
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;

    //In order to separate entities and value objects for the create responsibilities
    //we will develop the static create method for each entities under the model folder.
    public static Customer Create(CustomerId id, string name, string email)
    {
      ArgumentException.ThrowIfNullOrWhiteSpace(name);
      ArgumentException.ThrowIfNullOrWhiteSpace(email);

      var customer = new Customer
      {
        Id = id,
        Name = name,
        Email = email
      };

      return customer;
    }
  }
}
