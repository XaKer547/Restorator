using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Restorator.Desktop.Services;
using Restorator.Desktop.Session;
using Restorator.Desktop.ViewModels.Abstract;
using Restorator.Domain.Models;
using Restorator.Domain.Services;
using System.Collections.ObjectModel;

namespace Restorator.Desktop.ViewModels
{
    public partial class RestaurantManagementViewModel : ViewModelBase
    {
        private readonly IRestaurantService _restaurantService;
        private INavigationService _navigationService;
        public RestaurantManagementViewModel(IRestaurantService restaurantService,
                                             ISessionManager sessionManager,
                                             INavigationService navigationService)
        {
            _restaurantService = restaurantService;
            _navigationService = navigationService;

            _userId = sessionManager.GetSessionInfo().UserId;
        }

        [ObservableProperty]
        private ObservableCollection<RestaurantPreviewDTO> restaurantsPreview = [];

        [ObservableProperty]
        private bool searching;

        private readonly int _userId;

        [RelayCommand]
        public async Task LoadOwnedRestaurantsPreview()
        {
            Searching = true;

            RestaurantsPreview.Clear();

            var previews = await _restaurantService.GetOwnedRestaurantPreviews(new GetOwnedRestaurantsPreviewDTO() { UserId = _userId });

            foreach (var preview in previews)
                RestaurantsPreview.Add(preview);

            Searching = false;
        }

        [RelayCommand]
        public async Task OpenRestaurantEditor(RestaurantPreviewDTO restaurantPreview)
        {
            await _navigationService.NavigateWithHierarchyAsync<RestaurantEditorViewModel>(viewmodel => viewmodel.LoadRestaurantInfo(restaurantPreview.Id));
        }

        [RelayCommand]
        public async Task OpenRestaurantMaker()
        {
            await _navigationService.NavigateWithHierarchyAsync<RestaurantEditorViewModel>();
        }
    }
}
