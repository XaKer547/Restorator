using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Restorator.Desktop.Session;
using Restorator.Domain.Models;
using Restorator.Domain.Services;
using Wpf.Ui;

namespace Restorator.Desktop.ViewModels
{
    public partial class CreateRestaurantViewModel : RestaurantEditorViewModelBase
    {
        private readonly IRestaurantService _restaurantService;
        public CreateRestaurantViewModel(IRestaurantService restaurantService,
                                         Services.INavigationService navigationService,
                                         ISessionManager sessionManager,
                                         IContentDialogService contentDialogService) : base(restaurantService, navigationService, sessionManager, contentDialogService)
        {
            _restaurantService = restaurantService;
        }

        [ObservableProperty]
        private ObservableCollection<RestaurantTemplateDTO> templates = [];

        [ObservableProperty]
        private RestaurantTemplateDTO selectedTemplate;

        [RelayCommand]
        public async Task CreateRestaurant()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                return;
            }

            var result = await _restaurantService.CreateRestaurant(new CreateRestaurantDTO
            {
                UserId = _userId,
                Name = RestaurantName,
                BeginWorkTime = TimeOnly.FromDateTime(BeginWorkTime),
                EndWorkTime = TimeOnly.FromDateTime(EndWorkTime),
                Description = Description,
                Image = Image,
                Menu = Menu,
                Tags = RestaurantTags.Select(r => r.Id),
                TemplateId = SelectedTemplate.Id
            });
        }

        [RelayCommand]
        public async Task LoadRestaurantTemplates()
        {
            Templates.Clear();

            var templates = await _restaurantService.GetRestaurantTemplates();

            foreach (var template in templates)
                Templates.Add(template);
        }

        [RelayCommand]
        public async Task OpenExtendedRestaurantTemplate(RestaurantTemplateDTO restaurantTemplate)
        {
            //dialog

        }
    }
}