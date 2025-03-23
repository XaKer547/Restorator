using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Restorator.Desktop.Dialogs;
using Restorator.Desktop.Session;
using Restorator.Desktop.ViewModels.Abstract;
using Restorator.Domain.Services;
using Wpf.Ui;
using Wpf.Ui.Extensions;

namespace Restorator.Desktop.ViewModels
{
    public partial class RestaurantInfoViewModel : RestaurantViewModelBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly ISnackbarService _snackbarService;
        private readonly Services.INavigationService _navigationService;
        private readonly IContentDialogService _contentDialogService;
        private readonly ISessionManager _sessionManager;
        public RestaurantInfoViewModel(IRestaurantService restaurantService,
                                       ISnackbarService snackbarService,
                                       Services.INavigationService navigationService,
                                       IContentDialogService contentDialogService,
                                       ISessionManager sessionManager)
        {
            _restaurantService = restaurantService;
            _snackbarService = snackbarService;
            _navigationService = navigationService;
            _contentDialogService = contentDialogService;
            _sessionManager = sessionManager;
        }

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private byte[]? image;

        [ObservableProperty]
        private byte[]? menu;

        [ObservableProperty]
        private TimeOnly beginWorkTime;

        [ObservableProperty]
        private TimeOnly endWorkTime;

        private int _restaurantId;
        public async Task LoadRestaurantInfo(int restaurantId)
        {
            _restaurantId = restaurantId;

            var result = await _restaurantService.GetRestaurantInfo(_restaurantId);

            if (result.IsFailed)
            {
                _snackbarService.Show("Ошибка", "Что-то пошло не так", Wpf.Ui.Controls.ControlAppearance.Danger);

                await _navigationService.NavigateBackAsync();

                return;
            }

            var info = result.Value;

            Name = info.Name;
            Image = info.Image;
            Menu = info.Menu;
            Description = info.Description;
            BeginWorkTime = info.BeginWorkTime;
            EndWorkTime = info.EndWorkTime;
        }

        [RelayCommand]
        public async Task OpenRestaurantReservation()
        {
            if (!_sessionManager.TryGetSession(out var sessionInfo))
            {
                await _navigationService.NavigateWithHierarchyAsync<AuthenticationViewModel>();

                return;
            }
            
            //check role? nah

            await _navigationService.NavigateWithHierarchyAsync<RestaurantReservationViewModel>(viewModel => viewModel.LoadRestaurantPlan(_restaurantId));
        }

        [RelayCommand]
        public async Task ExpandRestaurantMenu()
        {
            await _contentDialogService.ShowAsync(new ExpandedRestaurantMenuDialog(Menu), new CancellationToken());
        }

        [RelayCommand]
        public async Task CloseRestaurantInfo()
        {
            await _navigationService.NavigateBackAsync();
        }
    }
}