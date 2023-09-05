/*
Dependency Inversion Principle

Classes de alto nível não deveriam depender de classes de baixo nível. Ambas devem depender de abstrações. As abstrações não devem depender de detalhes. Detalhes devem depender de abstrações.
*/

namespace Solid.DIP.NOK
{
  class MySQLDatabase
  {
    public void Insert() => Console.WriteLine("MYSQL insert");

    public void Update() => Console.WriteLine("MYSQL update");

    public void Delete() => Console.WriteLine("MYSQL delete");
  }

  class BudgetReport
  {
    public MySQLDatabase MySQL { get; set; } = new MySQLDatabase();

    public void Open(DateTime date) =>
      Console.WriteLine($"Open {date.ToShortDateString()}");

    public void Save() => Console.WriteLine("Save");
  }

  static class Test
  {
    public static void Run()
    {
      var mysql = new MySQLDatabase();
      var report = new BudgetReport();
      report.MySQL = mysql;
      report.Open(DateTime.Now);
    }
  }
}


namespace Solid.DIP.OK
{
  interface IDatabse
  {
    public void Insert();
    public void Update();
    public void Delete();
  }

  class BudgetReport : IDatabse
  {
    public IDatabse? Database { get; set; }

    public void Delete()
    {
      Console.WriteLine("Report delete");
      this.Database?.Delete();
    }

    public void Insert()
    {
      Console.WriteLine("Report insert");
      this.Database?.Insert();
    }

    public void Update()
    {
      Console.WriteLine("Report update");
      this.Database?.Update();
    }

    public void Open(DateTime date) =>
      Console.WriteLine($"Open {date.ToShortDateString()}");

    public void Save() => Console.WriteLine("Save");
  }

  class MySQL : IDatabse
  {
    public void Delete() => Console.WriteLine("MySQL delete");

    public void Insert() => Console.WriteLine("MySQL insert");

    public void Update() => Console.WriteLine("MySQL update");
  }

  class MongoDB : IDatabse
  {
    public void Delete() => Console.WriteLine("MongoDB delete");

    public void Insert() => Console.WriteLine("MongoDB insert");

    public void Update() => Console.WriteLine("MongoDB update");
  }

  static class Test
  {
    public static void Run()
    {
      var report = new BudgetReport();
      report.Database = new MySQL();
      report.Open(DateTime.Now);
      report.Insert();
      report.Save();
    }
  }
}
