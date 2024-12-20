using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Restorator.Desktop.ViewModels.Abstract;
using Restorator.Domain.Models;
using Restorator.Domain.Services;

namespace Restorator.Desktop.ViewModels
{
    public partial class RestaurantSearchViewModel : ViewModelBase
    {
        [ObservableProperty]
        private IReadOnlyCollection<string> _restaurantNames;

        [ObservableProperty]
        private IReadOnlyCollection<RestaurantPreviewDTO> _restaurantsPreview;

        [ObservableProperty]


        private readonly IRestaurantService _restaurantService;
        public RestaurantSearchViewModel(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [RelayCommand]
        public void SearchRestaurants()
        {

            int a = 1;
        }
    }
}
