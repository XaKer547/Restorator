using Microsoft.Extensions.DependencyInjection;
using Refit;
using Restorator.Application.Server.Services;
using Restorator.DataAccess.SqlServer;
using Restorator.Desktop.Controls;
using Restorator.Desktop.Infrastructure;
using Restorator.Desktop.Services;
using Restorator.Desktop.Session;
using Restorator.Desktop.ViewModels;
using Restorator.Desktop.ViewModels.Abstract;
using Restorator.Desktop.Views.Pages;
using Restorator.Desktop.Views.Windows;
using Restorator.Domain.Services;
using System.Reflection;
using Wpf.Ui;
using Wpf.Ui.Abstractions;
using Wpf.Ui.DependencyInjection;

namespace Restorator.Desktop.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection Configure(this IServiceCollection services)
        {
            return services.ConfigureServices()
                           .ConfigureApiClients()
                           .ConfigureViews();
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<ISnackbarService, SnackbarService>();
            services.AddSingleton<IContentDialogService, ContentDialogService>();
            services.AddSingleton<INavigationViewPageProvider, DependencyInjectionNavigationViewPageProvider>();
            services.AddSingleton<Services.INavigationService, Services.NavigationService>();
            services.AddSingleton<Wpf.Ui.INavigationService, Wpf.Ui.NavigationService>();
            services.AddSingleton<IPageService, PageService>();

            services.AddSingleton<ISessionManager, SessionManager>();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IReservationService, ReservationService>();

            services.AddRestoratorDbContext();

            return services;
        }

        public static IServiceCollection ConfigureApiClients(this IServiceCollection services)
        {
            var settings = new RefitSettings()
            {
                AuthorizationHeaderValueGetter = (message, cancellationToken) => Task.FromResult(Properties.Settings.Default.Token)
            };

            var apiBase = new Uri("https://10.173.99.217:7090");

            services.AddRefitClient<IAccountService>(settings)
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiBase, "account"));

            return services;
        }

        public static IServiceCollection ConfigureViews(this IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();

            services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton<RestaurantSearchViewModel>();

            var manager = new DataTemplateManager().RegisterDataTemplate<AuthenticationViewModel, AuthenticationPage>()
                                                   .RegisterDataTemplate<SignInViewModel, SignInControl>()
                                                   .RegisterDataTemplate<SignUpViewModel, SignUpControl>()
                                                   .RegisterDataTemplate<MenuViewModel, MenuPage>()
                                                   .RegisterDataTemplate<RestaurantInfoViewModel, RestaurantInfoPage>()
                                                   .RegisterDataTemplate<RestaurantReservationViewModel, RestraurantReservationPage>()
                                                   .RegisterDataTemplate<UserReservationsViewModel, UserReservationsPage>()
                                                   .RegisterDataTemplate<RestaurantSearchViewModel, RestaurantSearchPage>()
                                                   .RegisterDataTemplate<EditRestaurantViewModel, RestaurantEditorPage>()
                                                   .RegisterDataTemplate<CreateRestaurantViewModel, RestaurantMakerPage>()
                                                   .RegisterDataTemplate<RestaurantReservationsManagementViewModel, ReservsationsManagementPage>()
                                                   .RegisterDataTemplate<RestaurantsVerificationViewModel, RestaurantsVerificationPage>()
                                                   .RegisterDataTemplate<RestaurantVerificationViewModel, RestaurantVerificationPage>()
                                                   .RegisterDataTemplate<RestaurantTemplateGeneratorViewModel, RestaurantTemplateGeneratorPage>()
                                                   .RegisterDataTemplate<RestaurantManagementViewModel, RestaurantManagementPage>();

            manager.SetControlsCulture();

            var assembly = Assembly.GetExecutingAssembly();

            services.AddTransientFromNamespace("Restorator.Desktop.ViewModels", assembly);
            services.AddTransientFromNamespace("Restorator.Desktop.Views", assembly);

            var currentApp = System.Windows.Application.Current;

            currentApp.Startup += (assemb, args) => manager.InitilizeTemplates(currentApp.Resources);

            services.AddSingleton(manager);

            return services;
        }

        public static IServiceCollection AddTransientFromNamespace(this IServiceCollection services, string namespaceName, params Assembly[] assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                IEnumerable<Type> types = assembly.GetTypes()
                                                  .Where(x => x.IsClass && (x.Namespace?.StartsWith(namespaceName, StringComparison.InvariantCultureIgnoreCase) ?? false));

                foreach (var type in types.Where(type => services.All(x => x.ServiceType != type)))
                {
                    if (type == typeof(ViewModelBase))
                        continue;

                    _ = services.AddTransient(type);
                }
            }

            return services;
        }
    }
}
