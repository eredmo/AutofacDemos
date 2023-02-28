using System;
using System.Windows;
using AD11.Infrastructure;
using AD11.MainWindow;
using Autofac;
using Serilog;
using Serilog.Events;

[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]

namespace AD11
{
    internal static class Program
    {
        [STAThread]
        internal static void Main()
        {
            var loggerConfig = new LoggerConfiguration()
                .MinimumLevel.Is(LogEventLevel.Verbose)
                .WriteTo.Console();

            using var logger = loggerConfig.CreateLogger();

            var programLogger = logger.ForContext(typeof(Program));

            programLogger.Information("Starting application");

            try
            {
                var builder = new ContainerBuilder();
                builder.RegisterInstance(logger).As<ILogger>();
                builder.RegisterType<ViewModelRunner>().As<IViewModelRunner>().SingleInstance();
                builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>();
                builder.RegisterType<App>().As<IDispatcherHelper>().AsSelf().SingleInstance();

                using var container = builder.Build();

                var app = container.Resolve<App>();
                app.InitializeComponent();
                app.Run();
            }
            catch (Exception exception)
            {
                programLogger.Fatal(exception, "Application has crashed (outside of dispatcher)");
                Environment.ExitCode = 99;
            }
        }
    }
}
