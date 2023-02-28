// See https://aka.ms/new-console-template for more information

using Autofac;

try
{

    var cb = new ContainerBuilder();
    cb.RegisterInstance(new Writer("Aha!")).As<IWriter>();
    cb.RegisterType<UserEnumerator>().As<IUserEnumerator>();
    cb.RegisterType<DbFacade>();
    using var container = cb.Build();

    // Use container
    var writer = container.Resolve<IWriter>();
    writer.Write();

    // container.Resolve<IUserEnumerator>().WriteAllUsers();

    //var facade = container.Resolve<DbFacade>();
    //var data = await facade.GetFromDatabase();
    //using (var scope = container.BeginLifetimeScope(conf => conf.RegisterInstance(data)))
    //{
    //    scope.Resolve<IUserEnumerator>().WriteAllUsers();
    //}
}
catch (Exception? ex)
{
    while (ex != null)
    {
        Console.WriteLine(new string('-', 80));
        Console.WriteLine(ex.Message);
        ex = ex.InnerException;
    }
}


public interface IWriter
{
    void Write();
}

public class Writer : IWriter
{
    private readonly string _text;

    public Writer(string text)
    {
        _text = text;
    }

    public void Write()
    {
        Console.WriteLine(_text);
    }
}


public interface IDataFromDatabase
{
    IReadOnlyList<string> Users { get; }
    IReadOnlyList<string> Roles { get; }
}

public class DbFacade
{
    private class DataFromDatabase : IDataFromDatabase
    {
        public DataFromDatabase()
        {
            Users = new string[] { "Calvin", "Hobbes" };
            Roles = new string[] { "Supreme overlord", "Tigre numero uno" };
        }
        public IReadOnlyList<string> Users { get; }
        public IReadOnlyList<string> Roles { get; }
    }

    public async ValueTask<IDataFromDatabase> GetFromDatabase()
    {
        // simulating database call
        await Task.Delay(100);
        return new DataFromDatabase();
    }
}

public interface IUserEnumerator
{
    void WriteAllUsers();
}

public class UserEnumerator : IUserEnumerator
{
    private readonly IDataFromDatabase _data;

    public UserEnumerator(IDataFromDatabase data)
    {
        _data = data;
    }

    public void WriteAllUsers()
    {
        foreach (var user in _data.Users)
        {
            Console.WriteLine($"There is a user and his or her name is {user}!");
        }
    }
}