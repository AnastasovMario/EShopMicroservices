﻿

using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
  public class GetOrdersByNameHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
  {
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
      var orders = await dbContext.Orders
        .Include(o => o.OrderItems)
        .AsNoTracking() //Good optimisation when you are only reading data
        .Where(o => o.OrderName.Value.Contains(query.Name))
        .OrderBy(o => o.OrderName.Value)
        .ToListAsync(cancellationToken);

      return new GetOrdersByNameResult(orders.ToOrderDtoList());
    }
  }
}
