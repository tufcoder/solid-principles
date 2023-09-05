namespace Solid.SRP;

// Single Responsibility

// Uma classe deve ter apenas uma razão para mudar.

/*
A classe contém vários comportamentos diferentes
*/
class Employee
{
  private string _name = string.Empty;

  public string GetName() => this._name;

  public void PrintTimeSheetReport() =>
    Console.WriteLine("PrintTimeSheetReport");
}

/*
Resolva o problema movendo o comportamento relacionado aos relatórios para uma classe em separado. Essa mudança permite que você mova outras coisas relacionadas ao relatório para a nova classe.
*/
class TimeSheetReport
{
  public void Print() => Console.WriteLine("Print time sheet report");
}

static class Tests
{
  public static void Run()
  {
    var report = new TimeSheetReport();
    report.Print();
  }
}
