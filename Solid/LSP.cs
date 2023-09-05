/*
Liskov Substitution Principle

Quando estendendo uma classe, lembre-se que você deve ser capaz de passar objetos da subclasse em lugar de objetos da classe pai sem quebrar o código cliente.

O princípio de substituição é um conjunto de chagegens que ajudam a prever se uma cubclasse permanece compatível com o código que foi capaz de trabalhar com objetos da superclasse. Este conceito é crítico quando desenvolvendo bibliotecas e frameworks, porque suas classes serão usadas por outras pessoas cujo código você não pode diretamente acessar ou mudar.

* Os tipos de parâmetros em um método de uma subclasse devem coincidir ou serem mais abstratos que os tipos de parâmetros nos métodos da superclasse.

* O tipo de retorno de um método de uma subclasse deve coincidir ou ser um subtipo do tipo de retorno no método da superclasse.

* Um método em uma subclasse não deve lançar tipos de exceções que não são esperados que o método base lançaria.

* Uma subclasse não deve fortalecer pré-condições.

* Uma subclasse não deveria enfraquecer pós-condições.

* Invariantes de uma superclasse devem ser preservadas.

* Uma subclasse não deve mudar valores de campos privados da superclasse.
*/

namespace Solid.LSP
{
  class Document
  {
    public string? Data { get; set; }
    public string? Filename { get; set; }

    public void Open() => Console.WriteLine("Open doc");

    public virtual void Save() => Console.WriteLine("Save doc");
  }

  class ReadOnlyDocument : Document
  {
    public override void Save()
    {
      throw new Exception("Can't save readonly document"); //nonsense
    }
  }

  class Project
  {
    public List<Document> Documents { get; set; } = new List<Document>();

    public void OpenAll()
    {
      foreach (var doc in Documents)
      {
        doc.Open();
      }
    }

    public void SaveAll()
    {
      foreach (var doc in Documents)
      {
        if (!(doc is ReadOnlyDocument))
        {
          doc.Save();
          Console.WriteLine(doc.Filename);
        }
      }
    }
  }

  /*
  Salvar não faz sentido em um documento somente leitura, então a subclasse tenta resolver isso ao resetar o comportamento base no método sobrescrito.
  */

  static class Test
  {
    public static void Run()
    {
      var project = new Project()
      {
        Documents = new List<Document>()
        {
          new ReadOnlyDocument() { Filename = "doc1" },
          new Document() {Filename = "doc2"},
          new ReadOnlyDocument() {Filename = "doc3"}
        }
      };

      project.SaveAll();
    }
  }
}

namespace Solid.LSP.OK
{
  class Document
  {
    public string? Data { get; set; }
    public string? Filename { get; set; }

    public void Open() => Console.WriteLine($"Open doc {this.Filename}");
  }

  class WritableDocument : Document
  {
    public virtual void Save() => Console.WriteLine($"Save doc {base.Filename}");
  }

  class Project
  {
    private List<Document> _allDocuments = new List<Document>();

    public List<WritableDocument> WritableDocuments { get; set; } = new List<WritableDocument>();
    public List<Document> AllDocuments
    {
      get => _allDocuments;
      set
      {
        if (value is null)
          throw new NullReferenceException("Invalid null reference!");
        value.ForEach(item =>
        {
          if (item is WritableDocument)
            if (!WritableDocuments.Exists(doc => doc == item))
              WritableDocuments.Add((WritableDocument)item);
        });
        _allDocuments = value;
      }
    }

    public void OpenAll()
    {
      foreach (var doc in AllDocuments)
      {
        doc.Open();
      }
    }

    public void SaveAll()
    {
      foreach (var doc in WritableDocuments)
      {
        doc.Save();
      }
    }
  }

  static class Test
  {
    public static void Run()
    {
      var project = new Project();
      project.AllDocuments = new List<Document>()
      {
        new Document() {Filename = "doc1"},
        new WritableDocument() {Filename = "doc2"},
        new Document() {Filename = "doc3"}
      };
      project.OpenAll();
      project.SaveAll();
      Console.ReadLine();
    }
  }
}
