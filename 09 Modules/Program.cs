using AD09;
using Autofac;

var cb = new ContainerBuilder();
cb.RegisterInstance(new Options()).As<IInterestingNumbersOptions>();
cb.RegisterModule<InterestingNumbersModule>();
using var container = cb.Build();

var generator = container.Resolve<IInterestingNumbersGenerator>();
foreach (var num in generator.GetInterestingNumbers())
{
    Console.WriteLine($"Found an interesting number: {num}");
}

Console.ReadLine();

public sealed class Options : IInterestingNumbersOptions
{
    public bool IncludePrimes => true;
    public bool IncludeFibonacci => false;
    public bool IncludeSquares => false;
    public bool IncludePerfect => true;
    public bool IncludeTheAnswer => true;
}