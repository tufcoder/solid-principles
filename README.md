# SOLID PRINCIPLES

## Introduction

I started this project just to study about **SOLID Principles**.
This project uses **.NET 7**.

* Solid Principles
  * Single Responsibility
  * Open Closed
  * Liskov Substitution
  * Interface Segregation
  * Dependency Inversion

## How to clone the project?

```bash
git clone git@github.com:tufcoder/solid-principles.git
```

## How to Test the Program?

### Using Visual Studio Code or a Terminal

The project is a `Console Application` that runs in a `Terminal`.

1. Open the file `Program.cs` in the root directory and comment/uncomment the namespaces.
```csharp
// Solid.SRP.Tests.Run();
Solid.DIP.OK.Test.Run();
```
2. Save the file and open a Terminal
```bash
# optional
dotnet clean
dotnet build

# execute the application
dotnet run
```

### Using Visual Studio IDE or others

Open the solution file `solid-principles.sln`. Edit/save the file `Program.cs` and just click in `Run`.
The IDE will build and run the application in your default Terminal.
