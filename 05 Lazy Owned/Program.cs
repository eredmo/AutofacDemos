using Autofac;
using Autofac.Features.OwnedInstances;

namespace AD05;

internal static class Program
{
    internal static void Main()
    {
        // Build container
        var cb = new ContainerBuilder();
        cb.RegisterType<MyTestClass>();
        using var container = cb.Build();

        // Use container
        using (var lifetime = container.BeginLifetimeScope())
        {
            Console.WriteLine("Lifetime created");
            var x = lifetime.Resolve<Lazy<Owned<MyTestClass>>>();
            // var x = lifetime.Resolve<Lazy<MyTestClass>>();
            Console.WriteLine("Getting value from Lazy");
            var y = x.Value;
            Console.WriteLine("Got value from Lazy");
            y.Value.DoStuff();
            Console.WriteLine("Disposing...");
             y.Dispose();
            Console.WriteLine("Disposed!");
        }

        Console.ReadLine();
    }
}

public sealed class MyTestClass : IDisposable
{
    public MyTestClass()
    {
        Console.WriteLine("MyTestClass: Constructor running");
    }

    public void DoStuff()
    {
        Console.WriteLine("MyTestClass: Doing stuff");
    }

    public void Dispose()
    {
        Console.WriteLine("MyTestClass: Dispose running");
    }
}