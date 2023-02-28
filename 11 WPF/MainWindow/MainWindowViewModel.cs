using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using AD11.Infrastructure;
using Serilog;

namespace AD11.MainWindow
{
    public sealed class MainWindowViewModel : IMainWindowViewModel, INotifyPropertyChanged
    {
        private readonly ResourceDictionary _resourceDictionary;
        private readonly ILogger _logger;

        private readonly RelayCommand _setWhiteCommand;
        private readonly RelayCommand _setYellowCommand;
        private readonly RelayCommand _setBlueCommand;
        private readonly RelayCommand _sethGreenCommand;

        private Brush _background;

        public MainWindowViewModel(ResourceDictionary resourceDictionary, ILogger logger)
        {
            _resourceDictionary = resourceDictionary;
            _logger = logger.ForContext<MainWindowViewModel>();
            _setWhiteCommand = new RelayCommand(ExecuteSetWhiteCommand);
            _setYellowCommand = new RelayCommand(ExecuteSetYellowCommand);
            _setBlueCommand = new RelayCommand(ExecuteSetBlueCommand);
            _sethGreenCommand = new RelayCommand(ExecuteSethGreenCommand);
            _background = (Brush)_resourceDictionary["WhiteBrush"];
        }

        private void ExecuteSetWhiteCommand()
        {
            _logger.Information("Using the white brush");
            Background = (Brush)_resourceDictionary["WhiteBrush"];
        }

        private void ExecuteSetYellowCommand()
        {
            _logger.Information("Using the yellow brush");
            Background = (Brush)_resourceDictionary["YellowBrush"];
        }

        private void ExecuteSetBlueCommand()
        {
            _logger.Information("Using the blue brush");
            Background = (Brush)_resourceDictionary["BlueBrush"];
        }

        private void ExecuteSethGreenCommand()
        {
            _logger.Information("Using the green brush");
            Background = (Brush)_resourceDictionary["GreenBrush"];
        }

        public ICommand SetWhiteCommand => _setWhiteCommand;
        public ICommand SetYellowCommand => _setYellowCommand;
        public ICommand SetBlueCommand => _setBlueCommand;
        public ICommand SethGreenCommand => _sethGreenCommand;

        public Brush Background
        {
            get => _background;
            private set
            {
                if (_background != value)
                {
                    _background = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}