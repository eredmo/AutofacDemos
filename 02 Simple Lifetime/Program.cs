using Autofac;

namespace AD02;

internal static class Program
{
    internal static void Main()
    {
        // Build container
        var cb = new ContainerBuilder();
        cb.RegisterType<Greeter>();
        using IContainer container = cb.Build();

        // Create a lifetime
        using (var lifetime = container.BeginLifetimeScope())
        {
            for (int i = 0; i < 100; i++)
            {
                var greeter = lifetime.Resolve<Greeter>();
                greeter.Greet();
            }
        }

        Console.WriteLine("Done");
        Console.ReadLine();
    }
}


public sealed class Greeter //: IDisposable
{
    private readonly byte[] _tmp = new byte[100000000];

    public void Greet()
    {
        Console.WriteLine("Hello, World!");
    }

    //~Greeter()
    //{
    //    Console.WriteLine("Finalizer called...");
    //}

    //public void Dispose()
    //{
    //    GC.SuppressFinalize(this);
    //    Console.WriteLine("Dispose called...");
    //}
}