using Restorator.Desktop.ViewModels;
using Wpf.Ui;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace Restorator.Desktop.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : FluentWindow
    {
        public MainWindow(MainWindowViewModel viewModel,
            Services.INavigationService navigationService,
            IContentDialogService contentDialogService)
        {
            SystemThemeWatcher.Watch(this);

            DataContext = viewModel;

            InitializeComponent();

            navigationService.SetNavigationControl(RootNavigation);
            contentDialogService.SetDialogHost(RootContentDialog);

            navigationService.Navigate<RestaurantSearchViewModel>();
        }
    }
}