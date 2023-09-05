namespace Solid.OCP;

// Open/Closed Principle

// As classes devem ser abertas para extensão mas fechadas para modificação.

/*
Você tem que mudar a classe sempre que adicionar um novo método de envio na aplicação.
*/
class OldOrder
{
  private string _lineItems = string.Empty;
  private string _shipping = string.Empty;

  public void GetTotal() => Console.WriteLine("GetTotal");

  public void GetTotalWeight() => Console.WriteLine("GetTotalWeight");

  public void GetShippingType(string shippingType) =>
    Console.WriteLine("GetShippingType");

  public void GetShippingCost(string shipping)
  {
    Console.WriteLine("GetShippingCost");
    if (shipping.Equals("ground"))
      Console.WriteLine("shipping ground weight * 1.5");
    else if (shipping.Equals("air"))
      Console.WriteLine("shipping air weight * 3");
  }

  public void GetShippingDate() => Console.WriteLine("GetShippingDate");
}

/*
Você pode resolver o problema aplicando o padrão Strategy.
Comece extraindo os métodos de envio para classes separadas com uma interface comum.
*/

internal interface IShipping
{
  public void GetCost(Order order);
  public void GetDate(Order order);
}

class Ground : IShipping
{
  public void GetCost(Order order) => Console.WriteLine("Ground shipping weight * 1.5");

  public void GetDate(Order order) => Console.WriteLine("Ground date");
}

class Air : IShipping
{
  public void GetCost(Order order) => Console.WriteLine("Air shipping weight * 3");

  public void GetDate(Order order) => Console.WriteLine("Air date");
}

class Order
{
  private string _lineItems = string.Empty;
  private IShipping? _shipping;

  public Order(IShipping shipping) => this._shipping = shipping;

  public void GetTotal() => Console.WriteLine("GetTotal");

  public void GetTotalWeight() => Console.WriteLine("GetTotalWeight");

  public void GetShippingType(string shippingType) =>
    Console.WriteLine("GetShippingType");

  public void GetShippingCost() => this._shipping?.GetCost(this);

  public void GetShippingDate() => Console.WriteLine("GetShippingDate");
}

static class Test
{
  public static void Run()
  {
    var order = new OldOrder();
    order.GetShippingCost("air");
    order.GetShippingCost("ground");

    var groundOrder = new Order(new Ground());
    groundOrder.GetShippingCost();

    var airOrder = new Order(new Air());
    airOrder.GetShippingCost();
  }
}
