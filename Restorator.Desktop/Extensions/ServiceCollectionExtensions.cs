using Microsoft.Extensions.DependencyInjection;
using Restorator.Application.Server.Services;
using Restorator.Desktop.Controls;
using Restorator.Desktop.Infrastructure;
using Restorator.Desktop.Services;
using Restorator.Desktop.Session;
using Restorator.Desktop.ViewModels;
using Restorator.Desktop.ViewModels.Abstract;
using Restorator.Desktop.Views.Pages;
using Restorator.Desktop.Views.Windows;
using Restorator.Domain.Services;
using System.Net.Http;
using System.Reflection;
using Wpf.Ui;
using Wpf.Ui.Abstractions;
using Wpf.Ui.DependencyInjection;
using Restorator.DataAccess.SqlServer;

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

            return services;
        }
        public static IServiceCollection ConfigureApiClients(this IServiceCollection services)
        {
            Action<IServiceProvider, HttpClient, string?> configureClient = (provider, client, endpoint) =>
            {
                client.BaseAddress = new Uri($"https://localhost:7090/api/{endpoint}");

                var manager = provider.GetRequiredService<ISessionManager>();

                if (manager.TryGetToken(out var token))
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            };

            services.AddRestoratorDbContext();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IUserManager, UserManager>();


            //services.AddHttpClient<IAccountService, AccountService>((provider, client) => configureClient.Invoke(provider, client, "account/"));
            //services.AddHttpClient<IRestaurantService, RestaurantService>((provider, client) => configureClient.Invoke(provider, client, "restaurant/"));
            //services.AddHttpClient<IReservationService, ReservationService>((provider, client) => configureClient.Invoke(provider, client, "reservation/"));

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
