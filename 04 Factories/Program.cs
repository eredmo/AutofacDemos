using Autofac;

namespace AD04;

internal static class Program
{
    internal static void Main()
    {
        // Build container
        var cb = new ContainerBuilder();
        cb.RegisterType<Speech>().As<ISpeech>().SingleInstance();
        cb.RegisterType<SalesPerson>().As<ISalesPerson>();
        cb.RegisterType<Market>();

        using var container = cb.Build();

        // Use container
        var market = container.Resolve<Market>();
        market.Open();
        market.Run();

        Console.ReadLine();
    }
}

public interface ISpeech
{
    void Say(string value);
    void Shout(string value);
}

public class Speech : ISpeech
{
    public void Say(string value)
    {
        Console.WriteLine(value);
    }

    public void Shout(string value)
    {
        Console.WriteLine($"{value.ToUpper()}!!!!");
    }
}

public interface ISalesPerson
{
    void PromoteProduct();
}

public class SalesPerson : ISalesPerson, IDisposable
{
    private readonly string _line;
    private readonly bool _loud;
    private readonly ISpeech _speech;

    public SalesPerson(string line, bool loud, ISpeech speech)
    {
        _line = line;
        _loud = loud;
        _speech = speech;
    }

    public void PromoteProduct()
    {
        if (_loud)
        {
            _speech.Shout(_line);
        }
        else
        {
            _speech.Say(_line);
        }
    }

    public void Dispose()
    {
        Console.WriteLine("I'm going home...");
    }
}

public sealed class Market
{
    private readonly Func<string, bool, ISalesPerson> _factory;
    private readonly List<ISalesPerson> _salesPeople = new();

    public Market(Func<string, bool, ISalesPerson> factory)
    {
        _factory = factory;
    }

    public void Open()
    {
        _salesPeople.Add(_factory("Fresh eel pies", false));
        _salesPeople.Add(_factory("Buy the best Bread from Berta", true));
    }

    public void Run()
    {
        foreach (var salesPerson in _salesPeople)
        {
            salesPerson.PromoteProduct();
        }
    }
}