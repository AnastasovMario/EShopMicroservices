namespace Ordering.Infrastructure.Data.Extensions
{
  internal class InitialData
  {
    public static IEnumerable<Customer> Customers =>
      new List<Customer>
      {
        Customer.Create(CustomerId.Of(new Guid("cac68f31-9fcc-4e97-9da7-ba449949578e")), "mario", "mario@gmail.com"),
        Customer.Create(CustomerId.Of(new Guid("5e64aa17-89c3-45ef-baea-b03b85d0d1d1")), "kalina", "kalina@gmail.com")
      };

    public static IEnumerable<Product> Products =>
      new List<Product>
      {
        Product.Create(ProductId.Of(new Guid("bd597e39-d43e-4a5e-8fe1-43052d7a2e78")), "IPhone X", 500),
        Product.Create(ProductId.Of(new Guid("c89089a5-c3f1-4f52-b9cb-f0e9c70286f0")), "Samsung 10", 400),
        Product.Create(ProductId.Of(new Guid("07019366-c8c6-4fb0-894e-df2b738eef07")), "Huawei Plus", 650),
        Product.Create(ProductId.Of(new Guid("779ca668-c02b-47ae-b639-19b24bd19c98")), "Xiomi Mi", 450),
      };

    public static IEnumerable<Order> OrdersWithItems
    {
      get
      {
        var address1 = Address.Of("mario", "anastasov", "mario@gmail.com", "Bahcelievler No:4", "Sofia", "Bulgaria", "38050");
        var address2 = Address.Of("john", "doe", "john@gmail.com", "Broadway No:1", "England", "Nottingham", "08050");

        var payment1 = Payment.Of("mario", "5555555555554444", "12/28", "355", 1);
        var payment2 = Payment.Of("john", "8885555555554444", "06/30", "222", 2);

        var order1 = Order.Create(
                        OrderId.Of(Guid.NewGuid()),
                        CustomerId.Of(new Guid("cac68f31-9fcc-4e97-9da7-ba449949578e")),
                        OrderName.Of("ORD_1"),
                        shippingAddress: address1,
                        billingAddress: address1,
                        payment1);
        order1.Add(ProductId.Of(new Guid("bd597e39-d43e-4a5e-8fe1-43052d7a2e78")), 2, 500);
        order1.Add(ProductId.Of(new Guid("c89089a5-c3f1-4f52-b9cb-f0e9c70286f0")), 1, 400);

        var order2 = Order.Create(
                        OrderId.Of(Guid.NewGuid()),
                        CustomerId.Of(new Guid("5e64aa17-89c3-45ef-baea-b03b85d0d1d1")),
                        OrderName.Of("ORD_2"),
                        shippingAddress: address2,
                        billingAddress: address2,
                        payment2);
        order2.Add(ProductId.Of(new Guid("07019366-c8c6-4fb0-894e-df2b738eef07")), 1, 650);
        order2.Add(ProductId.Of(new Guid("779ca668-c02b-47ae-b639-19b24bd19c98")), 2, 450);

        return new List<Order> { order1, order2 };
      }
    }
  }
}
