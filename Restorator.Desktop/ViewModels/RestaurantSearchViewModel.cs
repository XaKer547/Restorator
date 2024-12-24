using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Restorator.Desktop.Dialogs;
using Restorator.Desktop.ViewModels.Abstract;
using Restorator.Domain.Models;
using Restorator.Domain.Services;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Restorator.Desktop.ViewModels
{
    public partial class RestaurantSearchViewModel : ViewModelBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly Services.INavigationService _navigationService;
        private readonly IContentDialogService _contentDialogService;
        public RestaurantSearchViewModel(IRestaurantService restaurantService,
                                         Services.INavigationService navigationService,
                                         RestaurantInfoViewModel restaurantInfo,
                                         RestaurantReservationViewModel reservationViewModel,
                                         IContentDialogService contentDialogService)
        {
            _restaurantService = restaurantService;
            _navigationService = navigationService;

            _restaurantInfoViewModel = restaurantInfo;
            _restaurantReservationViewModel = reservationViewModel;

            SelectedView = _restaurantInfoViewModel;
            _contentDialogService = contentDialogService;
        }

        [ObservableProperty]
        private IReadOnlyCollection<RestaurantSearchItemDTO> _restaurantNames;

        [ObservableProperty]
        private IReadOnlyCollection<RestaurantPreviewDTO> _restaurantsPreview;

        [ObservableProperty]
        private RestaurantPreviewDTO? _selectedRestaurantPreview;

        private RestaurantInfoViewModel _restaurantInfoViewModel;
        private RestaurantReservationViewModel _restaurantReservationViewModel;

        [ObservableProperty]
        private RestaurantViewModelBase selectedView;

        [ObservableProperty]
        private bool infoOpened = false;

        [ObservableProperty]
        private bool searching;

        private bool CanOpenRestaurantInfo => SelectedRestaurantPreview != null && !InfoOpened;
        [RelayCommand(CanExecute = nameof(CanOpenRestaurantInfo), AllowConcurrentExecutions = false)]
        public async Task OpenRestaurantInfo()
        {
            await OpenRestaurantInfo(SelectedRestaurantPreview!.Id);
        }

        private int _selectedRestaurantId;
        private async Task OpenRestaurantInfo(int id)
        {
            _selectedRestaurantId = id;

            InfoOpened = true;

            await _restaurantInfoViewModel.OpenRestaurantInfo(id);

            CloseSelectedViewCommand = CloseRestaurantInfoCommand;
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task OpenRestaurantInfoFromSuggestion(AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var item = (RestaurantSearchItemDTO)args.SelectedItem;

            if (item.Id != SelectedRestaurantPreview?.Id)
                await OpenRestaurantInfo(item.Id);
        }

        [RelayCommand]
        public async Task OpenRestaurantReservation()
        {
            //SelectedView = _restaurantReservationViewModel;

            await _restaurantReservationViewModel.LoadReservationPlan(_selectedRestaurantId);

            var dialog = new ReservationContentDialog(_restaurantReservationViewModel);

            await _contentDialogService.ShowAsync(dialog, new CancellationToken());

            //CloseSelectedViewCommand = CloseRestaurantReservationCommand;
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task SearchRestaurants()
        {
            SelectedRestaurantPreview = null;

            Searching = true;

            RestaurantNames = await _restaurantService.GetRestaurantNames();

            RestaurantsPreview = await _restaurantService.GetRestaurantPreviews(new GetRestaurantsPreviewDTO()
            {
                CurrentPage = 1,
                PageSize = 10
            });

            /*
            new List<string>()
            {
                "Синабонная Бо Синна",
                "Дорсия",
                "Под котлами",
                "Павлин-Мавлин"
            };
            new List<RestaurantPreviewDTO>()
            {
                new RestaurantPreviewDTO
                {
                    Id = 1,
                    Name = "Синабонная Бо Синна",
                    Description = "Погрузитесь в мир сладких грез и пряных ароматов в “Синабонной Бо Синна”, где каждый ролл создан с душой и любовью, вдохновлённый самим Бо Синна! Здесь каждое блюдо — это произведение кулинарного искусства, приготовленное с заботой о ваших вкусовых рецепторах. Мы специализируемся на классических и авторских синабонах, приготовленных из нежнейшего теста, щедро сдобренных ароматной корицей и сливочным кремом, в точности как это делал сам маэстро.",
                },
                new RestaurantPreviewDTO
                {
                    Id = 2,
                    Name = "Дорсия",
                    Description = "“Дорсия” – это не просто ресторан, это воплощение изысканности, амбиций и безупречного вкуса, место, где каждый вечер становится спектаклем. За этими стенами, среди приглушенного света и безукоризненного сервиса, не раз проводил свои вечера сам Патрик Бейтман. Здесь царит атмосфера утонченной роскоши, где каждый элемент – от хрустальных бокалов до минималистичных картин на стенах – тщательно отобран, чтобы создать идеальную сцену для наслаждения высокой кухней. Приходите и вы, чтобы почувствовать себя настоящим сигмой!",
                },
            };
            */

            Searching = false;
        }

        [ObservableProperty]
        private ICommand closeSelectedViewCommand;

        [RelayCommand]
        public void CloseRestaurantInfo()
        {
            InfoOpened = false;
            SelectedRestaurantPreview = null;
        }

        [RelayCommand]
        public void Logout()
        {
            //
        }
    }
}
