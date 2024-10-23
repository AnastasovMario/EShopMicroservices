using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations
{
  public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
  {
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
      builder.HasKey(oi => oi.Id);

      //we are saving this Id to Database, we will use value as a guide information into SQL
      //when we are reading these value from the database, we will convert them order item id with off method and converting the strongly typed Ids
      builder.Property(oi => oi.Id).HasConversion(
                              orderItemId => orderItemId.Value,
                              dbId => OrderItemId.Of(dbId));

      builder.HasOne<Product>()
        .WithMany()
        .HasForeignKey(oi => oi.ProductId);

      builder.Property(oi => oi.Quantity).IsRequired();

      builder.Property(oi => oi.Price).IsRequired();
    }
  }
}
