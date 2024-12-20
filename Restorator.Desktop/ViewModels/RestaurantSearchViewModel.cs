using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Restorator.Desktop.Notifications;
using Restorator.Desktop.Services;
using Restorator.Desktop.ViewModels.Abstract;
using Restorator.Domain.Models;
using Restorator.Domain.Services;

namespace Restorator.Desktop.ViewModels
{
    public partial class RestaurantSearchViewModel : ViewModelBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly INavigationService _navigationService;
        private readonly IMediator _mediator;

        public RestaurantSearchViewModel(IRestaurantService restaurantService,
                                         INavigationService navigationService,
                                         IMediator mediator)
        {
            _restaurantService = restaurantService;
            _navigationService = navigationService;
            _mediator = mediator;
        }

        [ObservableProperty]
        private IReadOnlyCollection<string> _restaurantNames;

        [ObservableProperty]
        private IReadOnlyCollection<RestaurantPreviewDTO> _restaurantsPreview;

        [ObservableProperty]
        private RestaurantPreviewDTO? _selectedRestaurantPreview;

        private bool CanOpenRestaurantInfo => SelectedRestaurantPreview != null;
        [RelayCommand(CanExecute = nameof(CanOpenRestaurantInfo))]
        public void OpenRestaurantInfo()
        {
            //open part and send data

            _mediator.Publish(new RestaurantInfoNoification(SelectedRestaurantPreview!.Id));
        }

        [RelayCommand]
        public void SearchRestaurants()
        {
            RestaurantNames = new List<string>()
            {
                "Синабонная Бо Синна"
            };

            RestaurantsPreview = new List<RestaurantPreviewDTO>()
            {
                new RestaurantPreviewDTO
                {
                    Id = 1,
                    Description = "Testsasdddddddddddddddddddddddddddddddddddd",
                    Name = "Синабонная Бо Синна"
                },
                new RestaurantPreviewDTO
                {
                    Id = 1,
                    Description = "Testsasdddddddddddddddddddddddddddddddddddd",
                    Name = "Синабонная Бо Синна"
                },
                new RestaurantPreviewDTO
                {
                    Id = 1,
                    Description = "Testsasdddddddddddddddddddddddddddddddddddd",
                    Name = "Синабонная Бо Синна"
                },
                new RestaurantPreviewDTO
                {
                    Id = 1,
                    Description = "Testsasdddddddddddddddddddddddddddddddddddd",
                    Name = "Синабонная Бо Синна"
                },
                new RestaurantPreviewDTO
                {
                    Id = 1,
                    Description = "Testsasdddddddddddddddddddddddddddddddddddd",
                    Name = "Синабонная Бо Синна"
                },
                new RestaurantPreviewDTO
                {
                    Id = 1,
                    Description = "Testsasdddddddddddddddddddddddddddddddddddd",
                    Name = "Синабонная Бо Синна"
                },
                new RestaurantPreviewDTO
                {
                    Id = 1,
                    Description = "Testsasdddddddddddddddddddddddddddddddddddd",
                    Name = "Синабонная Бо Синна"
                },
                new RestaurantPreviewDTO
                {
                    Id = 1,
                    Description = "Testsasdddddddddddddddddddddddddddddddddddd",
                    Name = "Синабонная Бо Синна"

                },
            };
        }

        [RelayCommand]
        public void Logout()
        {
            //
        }
    }
}
