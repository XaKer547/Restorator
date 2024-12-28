using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Restorator.DataAccess.Data.Entities.Enums;
using Restorator.Desktop.Dialogs;
using Restorator.Desktop.Session;
using Restorator.Desktop.ViewModels.Abstract;
using Restorator.Desktop.Views.Pages;
using System.Collections.ObjectModel;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Restorator.Desktop.ViewModels
{
    public partial class MenuViewModel : ViewModelBase
    {
        private readonly IPageService _pageService;
        private readonly Services.INavigationService _navigationService;
        private readonly ISessionManager _sessionManager;
        private readonly IContentDialogService _contentDialogService;

        public MenuViewModel(IPageService pageService,
                             Services.INavigationService navigationService,
                             ISessionManager sessionManager,
                             IContentDialogService contentDialogService)
        {
            _pageService = pageService;
            _navigationService = navigationService;
            _sessionManager = sessionManager;

            var session = _sessionManager.GetSessionInfo();

            Username = session.Username;

            _role = Enum.Parse<Roles>(session.Role);
            _contentDialogService = contentDialogService;
        }

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private ObservableCollection<object> menuItems = [];

        private readonly Roles _role;


        [RelayCommand]
        public void ConfigurePageService(INavigationView navigationView)
        {
            if (navigationView.MenuItems.Count == 0)
                InitializeNavigationItems();

            navigationView.SetPageService(_pageService);
        }

        private void InitializeNavigationItems()
        {
            if (_role == Roles.User)
            {
                MenuItems.Add(new NavigationViewItem("Поиск", SymbolRegular.Search16, typeof(RestaurantSearchPage)));
                MenuItems.Add(new NavigationViewItem("Бронирования", SymbolRegular.Sticker20, typeof(UserReservationsPage)));
            }

            if (_role == Roles.Manager)
            {
                MenuItems.Add(new NavigationViewItem("Управление", SymbolRegular.Sticker20, typeof(RestaurantManagementPage)));
            }

            if (_role == Roles.Admin)
            {
                MenuItems.Add(new NavigationViewItem("Управление", SymbolRegular.Sticker20, typeof(RestaurantManagementPage)));
            }
        }

        [RelayCommand]
        public async Task Logout()
        {
            var result = await _contentDialogService.ShowAsync(new ConfirmLogoutContentDialog(), new CancellationToken());

            if (result != ContentDialogResult.Primary)
                return;

            _sessionManager.RemoveSession();

            _navigationService.Navigate<AuthenticationViewModel>();
        }
    }
}