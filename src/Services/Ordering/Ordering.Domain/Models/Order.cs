using Ordering.Domain.Abstractions;

namespace Ordering.Domain.Models
{
  public class Order : Aggregate<Guid>
  {
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
  }
}
