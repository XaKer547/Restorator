using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Restorator.Desktop.Services;
using Restorator.Desktop.ViewModels.Abstract;
using Restorator.Domain.Models;
using Restorator.Domain.Services;
using System.Collections.ObjectModel;

namespace Restorator.Desktop.ViewModels
{
    public partial class RestaurantsVerificationViewModel : ViewModelBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly INavigationService _navigationService;
        public RestaurantsVerificationViewModel(IRestaurantService restaurantService,
                                                INavigationService navigationService)
        {
            _restaurantService = restaurantService;
            _navigationService = navigationService;
        }

        [ObservableProperty]
        private bool? showVerified = false;

        [ObservableProperty]
        private ObservableCollection<RestaurantPreviewDTO> previews = [];

        [RelayCommand]
        public async Task LoadRestaurantPreviews()
        {
            Previews.Clear();

            var previews = await _restaurantService.GetRestaurantPreviews(new GetRestaurantsPreviewDTO()
            {
                Filter = new GetRestaurantsPreviewFilter()
                {
                    RequireApproved = ShowVerified,
                }
            });

            foreach (var preview in previews)
            {
                Previews.Add(preview);
            }
        }

        async partial void OnShowVerifiedChanged(bool? value)
        {
            await LoadRestaurantPreviews();
        }


        [RelayCommand]
        public async Task OpenRestaurantVerification(RestaurantPreviewDTO restaurantPreview)
        {
            await _navigationService.NavigateWithHierarchyAsync<RestaurantVerificationViewModel>(viewmodel =>
            viewmodel.LoadRestaurantInfo(restaurantPreview.Id));
        }
    }
}
