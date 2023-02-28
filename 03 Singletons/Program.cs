using Autofac;

namespace AD03;

internal static class Program
{
    internal static void Main()
    {
        // Build container
        var cb = new ContainerBuilder();
        cb.RegisterType<ClassWithIdentity>().As<IWriteIdentity>();
        //cb.RegisterType<ClassWithIdentity>().As<IWriteIdentity>().SingleInstance();
        //cb.RegisterType<ClassWithIdentity>().As<IWriteIdentity>().InstancePerLifetimeScope();
        //cb.RegisterType<ClassWithIdentity>().As<IWriteIdentity>().InstancePerMatchingLifetimeScope("outer");
        //cb.RegisterType<ClassWithIdentity>().As<IWriteIdentity>().InstancePerMatchingLifetimeScope("inner");
        using IContainer container = cb.Build();

        // Create a lifetime
        using (var lifetime = container.BeginLifetimeScope("outer"))
        {
            Console.WriteLine("Creating first lifetime scope");
            var i1 = lifetime.Resolve<IWriteIdentity>();
            var i2 = lifetime.Resolve<IWriteIdentity>();

            i1.WriteIdentity();
            i2.WriteIdentity();
        }

        Console.WriteLine("Disposed of first lifetime scope");

        // Create a lifetime
        using (var lifetime = container.BeginLifetimeScope("outer"))
        {
            Console.WriteLine("Creating second lifetime scope");
            var i1 = lifetime.Resolve<IWriteIdentity>();
            var i2 = lifetime.Resolve<IWriteIdentity>();

            i1.WriteIdentity();
            i2.WriteIdentity();


            // Create a lifetime
            using (var innerLifetime = lifetime.BeginLifetimeScope("inner"))
            {
                Console.WriteLine("Creating inner lifetime scope");
                var i3 = innerLifetime.Resolve<IWriteIdentity>();
                var i4 = innerLifetime.Resolve<IWriteIdentity>();

                i3.WriteIdentity();
                i4.WriteIdentity();
            }

            Console.WriteLine("Disposed of inner lifetime scope");
        }
        Console.WriteLine("Disposed of second lifetime scope");


        Console.WriteLine("Done");
        Console.ReadLine();
    }
}

public interface IWriteIdentity
{
    void WriteIdentity();
}

public sealed class ClassWithIdentity: IWriteIdentity, IDisposable
{
    private static int _counter = 0;

    public int Id { get; }

    public ClassWithIdentity()
    {
        Id = _counter++;
        Console.WriteLine($"Created an instance of {nameof(ClassWithIdentity)} with id {Id}.");
    }

    public void WriteIdentity()
    {
        Console.WriteLine($"I'm an instance of {nameof(ClassWithIdentity)} and my id is {Id}.");
    }
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Console.WriteLine($"Dispose called for instance of {nameof(ClassWithIdentity)} with Id {Id}.");
    }
}