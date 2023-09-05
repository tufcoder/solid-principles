/*
Interface Segregation Principle

Clientes não devem ser forççados a depender de métodos que não usam.
*/

namespace Solid.ISP.NOK
{
  interface ICloudProvider
  {
    public void StoreFile(string name);

    public void GetFile(string name);

    public void CreateServer(string region);

    public void ListServers(string region);

    public void GetCDNAddress();
  }

  class Amazon : ICloudProvider
  {
    public void CreateServer(string region)
    {
      Console.WriteLine("Amazon create server");
    }

    public void GetCDNAddress()
    {
      Console.WriteLine("Amazon CDN address");
    }

    public void GetFile(string name)
    {
      Console.WriteLine("Amazon get file");
    }

    public void ListServers(string region)
    {
      Console.WriteLine("Amazon list servers");
    }

    public void StoreFile(string name)
    {
      Console.WriteLine("Amazon store file");
    }
  }

  class Dropbox : ICloudProvider
  {
    public void CreateServer(string region)
    {
      throw new NotImplementedException();
    }

    public void GetCDNAddress()
    {
      throw new NotImplementedException();
    }

    public void GetFile(string name)
    {
      Console.WriteLine("Dropbox get file");
    }

    public void ListServers(string region)
    {
      throw new NotImplementedException();
    }

    public void StoreFile(string name)
    {
      Console.WriteLine("Dropbox store file");
    }
  }

  static class Test
  {
    public static void Run()
    {
      var amazon = new Amazon();
      var dropbox = new Dropbox();

      amazon.CreateServer("amz-001");
      dropbox.CreateServer("dbx-001");
    }
  }
}


namespace Solid.ISP.OK
{
  interface ICloudHostingProvider
  {
    public void CreateServer(string region);
    public void ListServers(string region);
  }

  interface ICDNProvider
  {
    public void GetCDNAddress();
  }

  interface ICloudStorageProvider
  {
    public void StoreFile(string name);
    public void GetFile(string name);
  }

  class Amazon : ICloudHostingProvider, ICDNProvider, ICloudStorageProvider
  {
    public void CreateServer(string region) =>
      Console.WriteLine($"Amazon create server: {region}");

    public void GetCDNAddress() =>
      Console.WriteLine("Amazon CDN address");

    public void GetFile(string name) =>
      Console.WriteLine($"Amazon get file: {name}");

    public void ListServers(string region) =>
      Console.WriteLine($"Amazon list servers: {region}");

    public void StoreFile(string name) =>
      Console.WriteLine($"Amazon store file: {name}");
  }

  class Dropbox : ICloudStorageProvider
  {
    public void StoreFile(string name) =>
    Console.WriteLine($"Dropbox store file: {name}");

    public void GetFile(string name) =>
      Console.WriteLine($"Dropbox get file: {name}");
  }

  static class Test
  {
    public static void Run()
    {
      var amazon = new Amazon();
      var dropbox = new Dropbox();

      amazon.CreateServer("amz-001");
      dropbox.GetFile("file1");
    }
  }
}
