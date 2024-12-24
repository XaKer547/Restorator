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
                                         IMediator mediator,
                                         RestaurantPreviewViewModel restaurantPreview)
        {
            _restaurantService = restaurantService;
            _navigationService = navigationService;
            _mediator = mediator;
            RestaurantPreview = restaurantPreview;
        }


        [ObservableProperty]
        private IReadOnlyCollection<string> _restaurantNames;

        [ObservableProperty]
        private IReadOnlyCollection<RestaurantPreviewDTO> _restaurantsPreview;

        [ObservableProperty]
        private RestaurantPreviewDTO? _selectedRestaurantPreview;

        [ObservableProperty]
        private RestaurantPreviewViewModel restaurantPreview;


        private bool CanOpenRestaurantInfo => SelectedRestaurantPreview != null;
        [RelayCommand(CanExecute = nameof(CanOpenRestaurantInfo), AllowConcurrentExecutions = false)]
        public async Task OpenRestaurantInfo()
        {
            await RestaurantPreview.OpenRestaurantInfo(SelectedRestaurantPreview!.Id);
        }

        [RelayCommand]
        public void SearchRestaurants()
        {
            RestaurantNames = new List<string>()
            {
                "Синабонная Бо Синна",
                "Дорсия",
                "Под котлами",
                "Павлин-Мавлин"
            };

            RestaurantsPreview = new List<RestaurantPreviewDTO>()
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
        }

        [RelayCommand]
        public void Logout()
        {
            //
        }
    }
}
