namespace FactoryMethod.NOK
{
  class Transport
  {

  }

  class Truck : Transport
  {

  }

  class Ship : Transport
  {

  }

  abstract class Logistics
  {
    public void PlanDelivery()
    {
      Console.WriteLine("Logistics plan delivery");
      this.CreateTransport();
    }

    public abstract Transport CreateTransport();
  }

  class RoadLogistics : Logistics
  {
    public override Transport CreateTransport()
    {
      Console.WriteLine("Road create transport");
      return new Truck();
    }
  }

  class SeaLogistics : Logistics
  {
    public override Transport CreateTransport()
    {
      Console.WriteLine("Sea create transport");
      return new Ship();
    }
  }

  static class Test
  {
    public static void Run()
    {
      var truck = new RoadLogistics();
      truck.CreateTransport();

      var ship = new SeaLogistics();
      ship.CreateTransport();
    }
  }
}

namespace FactoryMethod.OK
{
  interface ITransport
  {
    public void Deliver();
  }

  class Truck : ITransport
  {
    public void Deliver()
    {
      Console.WriteLine("truck deliver");
    }
  }

  class Ship : ITransport
  {
    public void Deliver()
    {
      Console.WriteLine("ship deliver");
    }
  }

  abstract class Logistics
  {
    public void PlanDelivery()
    {
      Console.WriteLine("Logistics plan delivery");
      var transport = this.CreateTransport();
      transport.Deliver();
    }

    public abstract ITransport CreateTransport();
  }

  class RoadLogistics : Logistics
  {
    public override ITransport CreateTransport()
    {
      Console.WriteLine("Road create transport");
      return new Truck();
    }
  }

  class SeaLogistics : Logistics
  {
    public override ITransport CreateTransport()
    {
      Console.WriteLine("Sea create transport");
      return new Ship();
    }
  }

  static class Test
  {
    public static void Run()
    {
      var truck = new RoadLogistics();
      truck.PlanDelivery();

      var ship = new SeaLogistics();
      ship.PlanDelivery();
    }
  }
}

namespace FactoryMethod.Structure
{
  interface IProduct
  {
    public void DoStuff();
  }

  class ProductA : IProduct
  {
    public void DoStuff() => Console.WriteLine("A do stuff");
  }

  class ProductB : IProduct
  {
    public void DoStuff() => Console.WriteLine("B do stuff");
  }

  abstract class Creator
  {
    public void SomeOperation()
    {
      var product = this.CreateProduct();
      product.DoStuff();
    }

    protected abstract IProduct CreateProduct();
  }

  class CreatorA : Creator
  {
    protected override IProduct CreateProduct()
    {
      Console.WriteLine("CreatorA create product");
      return new ProductA();
    }
  }

  class CreatorB : Creator
  {
    protected override IProduct CreateProduct()
    {
      Console.WriteLine("CreatorB create product");
      return new ProductB();
    }
  }

  static class Test
  {
    public static void Run()
    {
      var creatorA = new CreatorA();
      creatorA.SomeOperation();

      var creatorB = new CreatorB();
      creatorB.SomeOperation();
    }
  }
}

namespace FactoryMethod.Dialog
{
  enum DialogEvent
  {
    Open,
    Close,
    Minimize,
    Maximize,
    Restore
  }

  enum ButtonType
  {
    OK,
    Cancel
  }

  interface IButton
  {
    public void Render();
    public void Render(ButtonType buttonType);
    public void OnClick();
    public void OnClick(DialogEvent dialogEvent);
  }

  class WindowsButton : IButton
  {
    public void Render() => Console.WriteLine("Windows button render");

    public void Render(ButtonType buttonType) =>
      Console.WriteLine($"Windows button render. Type: {buttonType}");

    public void OnClick() => Console.WriteLine("Windows button onclick");

    public void OnClick(DialogEvent dialogEvent) =>
      Console.WriteLine($"Windows button onclick dialog event: {dialogEvent}");
  }

  class WebButton : IButton
  {
    public void Render() => Console.WriteLine("Web button render");

    public void Render(ButtonType buttonType) =>
      Console.WriteLine($"Web button render. Type: {buttonType}");

    public void OnClick() => Console.WriteLine("Web button onclick");

    public void OnClick(DialogEvent dialogEvent) =>
      Console.WriteLine($"Web button onclick dialog event: {dialogEvent}");
  }

  abstract class Dialog
  {
    protected abstract IButton CreateButton();

    public virtual void Render()
    {
      var button = this.CreateButton();
      button.OnClick(DialogEvent.Close);
      button.Render(ButtonType.Cancel);
    }
  }

  class WindowsDialog : Dialog
  {
    protected override IButton CreateButton() => new WindowsButton();

    public override void Render()
    {
      var okButton = this.CreateButton();
      okButton.OnClick(DialogEvent.Close);
      okButton.Render(ButtonType.OK);
    }
  }

  class WebDialog : Dialog
  {
    protected override IButton CreateButton() => new WebButton();
  }

  static class Tests
  {
    private static Dialog? Dialog { get; set; }

    public static void Run()
    {
      Initialize();
      Dialog?.Render();
    }

    private static void Initialize()
    {
      string input = string.Empty;

      while (!(input.Equals("w") || input.Equals("web")))
      {
        Console.Write("Type (w)indows or (web)...(w/web)?: ");
        input = Console.ReadLine()?.ToLower() ?? string.Empty;

        if (input.Equals("w")) RenderWindows();
        else if (input.Equals("web")) RenderWeb();
        else Console.WriteLine("Choose an valid option: w or web");
      }
    }

    private static void RenderWindows() => Dialog = new WindowsDialog();

    private static void RenderWeb() => Dialog = new WebDialog();
  }
}

namespace FactoryMethod.Implementation
{
  interface IPayment
  {
    void Pay();
  }

  class CreditCard : IPayment
  {
    public void Pay() => Console.WriteLine("Pay with credit card");
  }

  class DebitCard : IPayment
  {
    public void Pay() => Console.WriteLine("Pay with debit card");
  }

  class Pix : IPayment
  {
    public void Pay() => Console.WriteLine("Pay with pix");
  }

  class BankSlip : IPayment
  {
    public void Pay() => Console.WriteLine("Pay with bank slip");
  }

  abstract class Payment
  {
    protected abstract IPayment CreatePayment();

    internal void Pay()
    {
      // IPayment payment = this.CreatePayment();
      var payment = this.CreatePayment();
      payment.Pay();
    }
  }

  class BankSlipPayment : Payment
  {
    protected override IPayment CreatePayment()
    {
      Console.WriteLine("Bank slip payment");
      return new BankSlip();
    }
  }

  class CreditCardPayment : Payment
  {
    protected override IPayment CreatePayment()
    {
      Console.WriteLine("Credit card payment");
      return new CreditCard();
    }
  }

  class DebitCardPayment : Payment
  {
    protected override IPayment CreatePayment()
    {
      Console.WriteLine("Debit card payment");
      return new DebitCard();
    }
  }

  class PixPayment : Payment
  {
    protected override IPayment CreatePayment()
    {
      Console.WriteLine("Pix payment");
      return new Pix();
    }
  }

  static class Tests
  {
    public static void Run()
    {
      var paymentCreditCard = new CreditCardPayment();
      paymentCreditCard.Pay();

      var paymentDebitCard = new DebitCardPayment();
      paymentDebitCard.Pay();

      var paymentPix = new PixPayment();
      paymentPix.Pay();

      var paymentBankslip = new BankSlipPayment();
      paymentBankslip.Pay();
    }
  }
}

namespace FactoryMethod.Conceptual
{
  /// <summary>
  /// The Product interface declares the operations that all concrete products
  /// must implement.
  /// </summary>
  interface IProduct
  {
    string Operation();
  }

  /// <summary>
  /// Concrete Products provide various implementations of the Product interface.
  /// </summary>
  class ConcreteProdut1 : IProduct
  {
    public string Operation() => "Result of ConcretProduct1";
  }

  class ConcreteProdut2 : IProduct
  {
    public string Operation() => "Result of ConcretProduct1";
  }

  /// <summary>
  /// The creator class declares the factory method that is supposed to return
  /// an object of a Product class. The Creator's subclasses usually provide
  /// the implementation of this method.
  /// </summary>
  abstract class Creator
  {
    /// <summary>
    /// Note that the Creator may also provide some default implementation of
    /// the factory method.
    /// </summary>
    /// <returns></returns>
    public abstract IProduct FactoryMethod();

    /// <summary>
    /// Also note that, despite its name, the Creator's primary responsibility
    /// is not creating products. Usually, it contains some core business logic
    /// that relies on Product objects, returned by the factory method.
    /// Subclasses can indirectly change that business logic by overriding the
    /// factory method and returning a different type of product from it.
    /// </summary>
    /// <returns></returns>
    public string SomeOperation()
    {
      // Call the factory method to crate a Product object.
      var product = this.FactoryMethod();
      // Now, use the product.
      var result = "Creator: The same creator's code has just worked with " +
        product.Operation();
      return result;
    }
  }

  /// <summary>
  /// Concrete Creators override the factory method in order to change the
  /// resulting product's type.
  /// </summary>
  class ConcreteCreator1 : Creator
  {
    /// <summary>
    /// Note that the signature of the method still uses the abstract product
    /// type, even though the concrete product is actually returned from the
    /// method. This way the Creator can stay independent of concrete product
    /// classes.
    /// </summary>
    /// <returns>A new instance of ConcreteProduct1</returns>
    public override IProduct FactoryMethod() => new ConcreteProdut1();
  }

  class ConcreteCreator2 : Creator
  {
    /// <summary>
    /// </summary>
    /// <returns>A new instance of ConcreteProduct2</returns>
    public override IProduct FactoryMethod() => new ConcreteProdut2();
  }

  /// <summary>
  /// The application picks a creator's type depending on the configuration or
  /// environment.
  /// </summary>
  static class Client
  {
    internal static void Run()
    {
      Console.WriteLine("App: Launched with the ConcreteCreator1.");
      ClientCode(new ConcreteCreator1());

      Console.WriteLine("");

      Console.WriteLine("App: Launched with the ConcreteCreator2.");
      ClientCode(new ConcreteCreator2());
    }

    /// <summary>
    /// The client code works with an instance of a concrete creator, albeit
    /// through its base interface. As long as the client keeps working with the
    /// creator via the base interface, you can pass it any creator's subclass.
    /// </summary>
    private static void ClientCode(Creator creator)
    {
      Console.WriteLine("Client: I'm not aware of the creator's class, " +
       "but it still works.\n" + creator.SomeOperation());
    }
  }

  static class Program
  {
    internal static void Run() => Client.Run();
  }
}
