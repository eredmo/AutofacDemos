using Autofac;

namespace AD01;

internal static class Program
{
    internal static void Main()
    {
        // Build container
        var cb = new ContainerBuilder();
        cb.RegisterType<Greeter>();
        using var container = cb.Build();

        // Use container
        var greeter = container.Resolve<Greeter>();
        greeter.Greet();
    }
}

public sealed class Greeter
{
    public void Greet()
    {
        Console.WriteLine("Hello, World!");
    }
}
