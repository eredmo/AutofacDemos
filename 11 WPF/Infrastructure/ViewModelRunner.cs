using System;
using AD11.MainWindow;

namespace AD11.Infrastructure
{
    public class ViewModelRunner : IViewModelRunner
    {
        public bool? RunViewModelAsDialog(object viewModel)
        {
            if (viewModel is IMainWindowViewModel mainWindowViewModel)
            {
                var view = new MainWindowView
                {
                    DataContext = mainWindowViewModel
                };

                return view.ShowDialog();
            }

            throw new ArgumentException("Unknown view model type", nameof(viewModel));
        }
    }
}