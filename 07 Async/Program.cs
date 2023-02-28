using Autofac;

// Build container
var cb = new ContainerBuilder();
cb.RegisterType<MyClass>();
cb.RegisterType<MyClass2>();
using var container = cb.Build();

Console.WriteLine("Creating instance and disposing of lifetime synchronously");
using (var lifetime = container.BeginLifetimeScope())
{
    var a = lifetime.Resolve<MyClass>();
    var b = lifetime.Resolve<MyClass2>();
}

Console.WriteLine("Creating instance and disposing of lifetime asynchronously");
await using (var lifetime = container.BeginLifetimeScope())
{
    var a = lifetime.Resolve<MyClass>();
    var b = lifetime.Resolve<MyClass2>();
}

public sealed class MyClass : IDisposable, IAsyncDisposable
{
    public void Dispose()
    {
        Console.WriteLine("Dispose called on MyClass");
    }

    public ValueTask DisposeAsync()
    {
        Console.WriteLine("DisposeAsync called on MyClass");
        return ValueTask.CompletedTask;
    }
}

public sealed class MyClass2 : IDisposable
{
    public void Dispose()
    {
        Console.WriteLine("Dispose called on MyClass2");
    }
}


