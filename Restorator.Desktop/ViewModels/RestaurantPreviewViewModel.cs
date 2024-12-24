using System.Runtime.InteropServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Restorator.Desktop.ViewModels.Abstract;
using Restorator.Domain.Services;
using Wpf.Ui;
using Wpf.Ui.Extensions;

namespace Restorator.Desktop.ViewModels
{
    public partial class RestaurantPreviewViewModel : ViewModelBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly ISnackbarService _snackbarService;
        public RestaurantPreviewViewModel(IRestaurantService restaurantService, ISnackbarService snackbarService)
        {
            _restaurantService = restaurantService;
            _snackbarService = snackbarService;
        }

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private TimeOnly beginWorkTime;

        [ObservableProperty]
        private TimeOnly endWorkTime;

        [RelayCommand]
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
    }
}
