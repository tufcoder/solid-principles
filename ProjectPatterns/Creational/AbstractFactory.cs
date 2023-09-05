namespace AbstractFactory.Pseudocode
{
  interface ICommonProperties
  {
    public string? Id { get; set; }
    public string? Label { get; set; }
  }

  /// <summary>
  /// Cada produto distinto de uma família de produtos deve ter uma interface
  /// base. Todas as variantes do produto devem implementar essa interface.
  /// </summary>
  abstract class Button : ICommonProperties
  {
    public string? Id { get; set; }
    public string? Label { get; set; }

    internal abstract void Paint();
  }

  /// <summary>
  /// Aqui está a interface base de outro produto. Todos os produtos podem
  /// interagir entre si, mas a interação apropriada só é possível entre
  /// produtos da mesma variante concreta.
  /// </summary>
  abstract class Checkbox : ICommonProperties
  {
    public string? Id { get; set; }
    public string? Label { get; set; }

    internal abstract void Paint();
  }

  /// <summary>
  /// Produtos concretos são criados por fábricas concretas correspondentes.
  /// </summary>
  class WinButton : Button
  {
    internal override void Paint()
    {
      Console.WriteLine("Paint win button");
    }
  }

  class MacButon : Button
  {
    internal override void Paint()
    {
      Console.WriteLine("Paint mac button");
    }
  }

  class WinCheckbox : Checkbox
  {
    internal override void Paint()
    {
      Console.WriteLine("Paint win checkbox");
    }
  }

  class MacCheckbox : Checkbox
  {
    internal override void Paint()
    {
      Console.WriteLine("Paint mac checkbox");
    }
  }

  /// <summary>
  /// A interface fábrica abstrata declara um conjunto de métodos que retorna
  /// diferentes produtos abstratos. Estes produtos são chamados uma família e
  /// estão relacionados por um tema ou conceito de alto nível. Produtos de uma
  /// família são geralmente capaxes de colaborar entre si. Uma família de
  /// produtos de uma variante são incompatíveis com os produtos de outro
  /// variante.
  /// </summary>
  interface IGUIFactory
  {
    Button CreateButton();
    Checkbox CreateCheckbox();
  }

  /// <summary>
  /// As fábricas concretas produzem uma família de produtos que pertencem a
  /// uma única variante. A fábrica garante que os produtos resultantes sejam
  /// compatíveis. Assinaturas dos métodos fábrica retornam um produto abstrato,
  /// enquanto que dentro do método um produto concreto é instanciado.
  /// </summary>
  class WinFactory : IGUIFactory
  {
    public Button CreateButton() => new WinButton();

    public Checkbox CreateCheckbox() => new WinCheckbox();
  }

  // Cada fábrica concreta tem uma variante de produto correspondente.
  class MacFactory : IGUIFactory
  {
    public Button CreateButton() => new MacButon();

    public Checkbox CreateCheckbox() => new MacCheckbox();
  }

  /// <summary>
  /// O código cliente trabalha com fábricas e produtos apenas através de tipos
  /// abstratos: IGUIFactory, Button, Checkbox.
  /// Isso permite que você passe qualquer subclasse fábrica ou de produto para
  /// o código cliente sem quebrá-lo.
  /// </summary>
  class Application
  {
    private IGUIFactory? Factory { get; set; }
    private Button? Button { get; set; }
    public Checkbox? Checkbox { get; set; }

    public Application(IGUIFactory factory) =>
      this.Factory = factory;

    public void CreateUI()
    {
      this.Button = Factory?.CreateButton();
      this.Checkbox = Factory?.CreateCheckbox();
    }

    public void Paint()
    {
      this.Button?.Paint();
      this.Checkbox?.Paint();
    }
  }

  static class ApplicationConfigurator
  {
    public static void Config()
    {
      string input = string.Empty;
      IGUIFactory? factory = null;

      while (!input.Equals("0"))
      {
        Console.Write("Type (win)dows or (mac)...(win/mac)? ");
        input = Console.ReadLine()?.ToLower() ?? string.Empty;

        switch (input)
        {
          case "win": factory = new WinFactory(); break;
          case "mac": factory = new MacFactory(); break;
          case "0": Exit(); factory = null; break;
          default: Console.WriteLine("Choose an valid option!"); break;
        }

        if (factory != null)
        {
          var app = new Application(factory);
          app.CreateUI();
          app.Paint();
        }
      }
    }

    private static void Exit() =>
      Console.WriteLine("Thanks for using the program!");
  }
}
