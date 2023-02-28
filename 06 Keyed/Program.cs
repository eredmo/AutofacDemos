// See https://aka.ms/new-console-template for more information

using Autofac;
using Autofac.Features.Indexed;

// Build container
var cb = new ContainerBuilder();
cb.RegisterType<LogToFile>().Keyed<IExceptionLogger>(LoggerTypes.File); //.As<IExceptionLogger>();
cb.RegisterType<LogToSplunk>().Keyed<IExceptionLogger>(LoggerTypes.Splunk); //.As<IExceptionLogger>();
cb.RegisterType<Failure>();

using var container = cb.Build();

// Use container
var greeter = container.Resolve<Failure>();
greeter.Run();

Console.ReadLine();


public class Failure
{
    private readonly IExceptionLogger _logger;

    public Failure(IIndex<LoggerTypes, IExceptionLogger> loggerIndex)
    {
        _logger = loggerIndex[LoggerTypes.Splunk];
    }

    //public Failure(IEnumerable<IExceptionLogger> loggers)
    //{
    //    _logger = loggers.First();
    //}

    public void Run()
    {
        try
        {
            var a = 0;
            var b = a / a;
            Console.WriteLine(b);
        }
        catch (Exception e)
        {
            _logger.Log(e);
        }
    }
}


public enum LoggerTypes
{
    File,
    Splunk
}

public interface IExceptionLogger
{
    void Log(Exception exception);
}

public class LogToFile : IExceptionLogger
{
    public void Log(Exception exception)
    {
        Console.WriteLine($"'{exception.Message}' logged to file...");
    }
}

public class LogToSplunk : IExceptionLogger
{
    public void Log(Exception exception)
    {
        Console.WriteLine($"'{exception.Message}' logged to splunk...");
    }
}


