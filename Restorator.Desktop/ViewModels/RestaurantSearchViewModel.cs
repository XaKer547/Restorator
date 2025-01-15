using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Restorator.Desktop.ViewModels.Abstract;
using Restorator.Domain.Models;
using Restorator.Domain.Services;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace Restorator.Desktop.ViewModels
{
    public partial class RestaurantSearchViewModel : ViewModelBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly Services.INavigationService _navigationService;
        public RestaurantSearchViewModel(IRestaurantService restaurantService,
                                         Services.INavigationService navigationService)
        {
            _restaurantService = restaurantService;
            _navigationService = navigationService;
        }

        [ObservableProperty]
        private IReadOnlyCollection<RestaurantSearchItemDTO> _restaurantsName;

        [ObservableProperty]
        private IReadOnlyCollection<RestaurantTagDTO> _restaurantsTag;

        [ObservableProperty]
        private ObservableCollection<RestaurantPreviewDTO> _restaurantsPreview = [];

        [ObservableProperty]
        private RestaurantTagDTO? _selectedTag;

        [ObservableProperty]
        private bool searching;

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task OpenRestaurantInfo(RestaurantPreviewDTO restaurantPreview)
        {
            await OpenRestaurantInfo(restaurantPreview!.Id);
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task OpenRestaurantInfoFromSuggestion(AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var item = (RestaurantSearchItemDTO)args.SelectedItem;

            await OpenRestaurantInfo(item.Id);
        }
        private async Task OpenRestaurantInfo(int id)
        {
            await _navigationService.NavigateWithHierarchyAsync<RestaurantInfoViewModel>(viewmodel => viewmodel.LoadRestaurantInfo(id));
        }

        private int _currentPage;
        private bool CanLoadRestaurants { get; set; }

        [RelayCommand(CanExecute = nameof(CanInitialize))]
        public async Task InitializeViewModel()
        {
            SelectedTag = null;

            Searching = true;

            _currentPage = 1;

            RestaurantsName = await _restaurantService.GetRestaurantNames();

            RestaurantsTag = await _restaurantService.GetRestaurantTags();

            await SearchRestaurants();

            Searching = false;

            Initialized = true;
        }

        [ObservableProperty]
        private bool tagEmpty = true;

        [RelayCommand]
        public async Task ChangeSearchTag(RestaurantTagDTO restaurantTag)
        {
            if (SelectedTag == restaurantTag)
            {
                SelectedTag = null;

                TagEmpty = true;
            }
            else
            {
                TagEmpty = false;

                SelectedTag = restaurantTag;
            }

            await ResetSearch();
        }

        [RelayCommand]
        public async Task ResetSearch()
        {
            _currentPage = 1;

            RestaurantsPreview.Clear();

            await SearchRestaurants();
        }

        [RelayCommand]
        public async Task ResetSelectedTag()
        {
            if (SelectedTag == null)
                return;

            SelectedTag = null;

            await ResetSearch();
        }

        [RelayCommand(AllowConcurrentExecutions = false, CanExecute = nameof(CanLoadRestaurants))]
        public async Task SearchRestaurants()
        {
            var restaurants = await _restaurantService.GetRestaurantPreviews(new GetRestaurantsPreviewDTO()
            {
                Filter = new GetRestaurantsPreviewFilter()
                {
                    RequireApproved = true,
                    Tag = SelectedTag,
                },
                PaginationFilter = new PaginationFilter()
                {
                    CurrentPage = _currentPage,
                    PageSize = 20
                }
            });

            _currentPage++;
            
            //пробуй)

            CanLoadRestaurants = restaurants.HasNextPage; 

            foreach (var restaurant in restaurants)
                RestaurantsPreview.Add(restaurant);
        }
    }
}