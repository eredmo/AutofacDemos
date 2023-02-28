using System;
using System.Windows;
using AD11.Infrastructure;
using AD11.MainWindow;
using Serilog;

namespace AD11
{
    public partial class App
    {
        private readonly ILogger _logger;
        private readonly IViewModelRunner _viewModelRunner;
        private readonly Func<ResourceDictionary, IMainWindowViewModel> _mainWindowViewModelFactory;

        public App()
        {
            throw new InvalidOperationException("This constructor should not be used.");
        }

        public App(ILogger logger, IViewModelRunner viewModelRunner, Func<ResourceDictionary, IMainWindowViewModel> mainWindowViewModelFactory)
        {
            _viewModelRunner = viewModelRunner;
            _mainWindowViewModelFactory = mainWindowViewModelFactory;
            _logger = logger.ForContext<App>();
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                var mainWindowViewModel = _mainWindowViewModelFactory(Resources);
                _viewModelRunner.RunViewModelAsDialog(mainWindowViewModel);
            }
            catch (Exception exception)
            {
                _logger.Fatal(exception, "Application has crashed (inside dispatcher)");
                Environment.ExitCode = 99;
            }
            finally
            {
                Shutdown();
            }
        }
    }
}
