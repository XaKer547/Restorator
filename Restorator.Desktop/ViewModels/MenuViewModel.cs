using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Restorator.DataAccess.Data.Entities.Enums;
using Restorator.Desktop.Dialogs;
using Restorator.Desktop.Session;
using Restorator.Desktop.ViewModels.Abstract;
using Restorator.Desktop.Views.Pages;
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
        private readonly INavigationService _menuNavigationService;

        public MenuViewModel(IPageService pageService,
                             Services.INavigationService navigationService,
                             ISessionManager sessionManager,
                             IContentDialogService contentDialogService,
                             INavigationService menuNavigationService)
        {
            _pageService = pageService;
            _navigationService = navigationService;
            _sessionManager = sessionManager;


            if (!_sessionManager.HaveSession())
            {
                Username = "Гость";
                _role = null;
            }
            else
            {
                var session = _sessionManager.GetSessionInfo();

                _role = Enum.Parse<Roles>(session.Role);
                Username = session.Username;
            }


            _contentDialogService = contentDialogService;
            _menuNavigationService = menuNavigationService;
        }

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private ObservableCollection<object> menuItems = [];

        [ObservableProperty]
        private ObservableCollection<object> footerItems = [];

        private Roles? _role;

        [RelayCommand]
        public void ConfigurePageService(INavigationView navigationView)
        {
            navigationView.SetPageService(_pageService);

            _menuNavigationService.SetNavigationControl(navigationView);

            if (navigationView.MenuItems.Count == 0)
                InitializeNavigationItems();

            var item = (NavigationViewItem)MenuItems.First();

            _menuNavigationService.Navigate(item.TargetPageType);
        }

        private void InitializeNavigationItems()
        {
            if (_role == Roles.User)
            {
                MenuItems.Add(new NavigationViewItem("Поиск", SymbolRegular.Search16, typeof(RestaurantSearchPage)));
                MenuItems.Add(new NavigationViewItem("Бронирования", SymbolRegular.BookOpen16, typeof(UserReservationsPage)));
            }

            if (_role == Roles.Manager)
            {
                MenuItems.Add(new NavigationViewItem("Управление", SymbolRegular.FolderPeople20, typeof(RestaurantManagementPage)));
            }

            if (_role == Roles.Admin)
            {
                MenuItems.Add(new NavigationViewItem("Заявки", SymbolRegular.TaskListRtl20, typeof(RestaurantsVerificationPage)));
            }

            if (_role is null)
            {
                MenuItems.Add(new NavigationViewItem("Поиск", SymbolRegular.Search16, typeof(RestaurantSearchPage)));

                FooterItems.Add(new NavigationViewItem
                {
                    Icon = new SymbolIcon(SymbolRegular.DoorArrowRight16),
                    Content = "Войти",
                    Command = LoginCommand
                });
            }
            else
            {
                FooterItems.Add(new NavigationViewItem
                {
                    Icon = new SymbolIcon(SymbolRegular.DoorArrowLeft16),
                    Content = "Выйти",
                    Command = LogoutCommand
                });
            }
        }

        [RelayCommand]
        public async Task Login()
        {
            await _navigationService.NavigateWithHierarchyAsync<AuthenticationViewModel>();
        }

        [RelayCommand]
        public async Task Logout()
        {
            var result = await _contentDialogService.ShowAsync(new ConfirmLogoutContentDialog(), new CancellationToken());

            if (result != ContentDialogResult.Primary)
                return;

            _sessionManager.RemoveSession();

            MenuItems.Clear();
            FooterItems.Clear();

            _role = null;
            Username = "Гость";

            InitializeNavigationItems();

            var item = (NavigationViewItem)MenuItems.First();

            _menuNavigationService.Navigate(item.TargetPageType);
        }
    }
}