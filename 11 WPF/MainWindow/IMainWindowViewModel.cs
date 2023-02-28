using System.Windows.Input;
using System.Windows.Media;

namespace AD11.MainWindow
{
    public interface IMainWindowViewModel
    {
        ICommand SetWhiteCommand { get; }
        ICommand SetYellowCommand { get; }
        ICommand SetBlueCommand { get; }
        ICommand SethGreenCommand { get; }

        Brush Background { get; }
    }
}
