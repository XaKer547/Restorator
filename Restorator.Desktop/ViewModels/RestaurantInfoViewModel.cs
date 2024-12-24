using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

        public RestaurantInfoViewModel(IRestaurantService restaurantService,
                                       ISnackbarService snackbarService,
                                       Services.INavigationService navigationService)
        {
            _restaurantService = restaurantService;
            _snackbarService = snackbarService;
            _navigationService = navigationService;
        }

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private TimeOnly beginWorkTime;

        [ObservableProperty]
        private TimeOnly endWorkTime;

        public async Task OpenRestaurantInfo(int restaurantId)
        {
            var result = await _restaurantService.GetRestaurantInfo(restaurantId);

            if (result.IsFailed)
            {
                _snackbarService.Show("Ой-ой", "Что-то пошло не так", Wpf.Ui.Controls.ControlAppearance.Danger);

                return;
            }

            var info = result.Value;

            Name = info.Name;
            Description = info.Description;
            BeginWorkTime = info.BeginWorkTime;
            EndWorkTime = info.EndWorkTime;
        }

        [RelayCommand]
        public void OpenRestaurantReservation()
        {
            _navigationService.Navigate<RestaurantReservationViewModel>();
        }
    }
}
