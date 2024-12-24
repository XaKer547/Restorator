using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Restorator.Application.Services;
using Restorator.DataAccess.Data;
using Restorator.Desktop.Controls;
using Restorator.Desktop.Infrastructure;
using Restorator.Desktop.Session;
using Restorator.Desktop.ViewModels;
using Restorator.Desktop.ViewModels.Abstract;
using Restorator.Desktop.Views.Pages;
using Restorator.Desktop.Views.Windows;
using Restorator.Domain.Services;
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
                .ConfigureViews();
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<ISnackbarService, SnackbarService>();
            services.AddSingleton<IContentDialogService, ContentDialogService>();
            services.AddSingleton<INavigationViewPageProvider, DependencyInjectionNavigationViewPageProvider>();
            services.AddSingleton<Services.INavigationService, Services.NavigationService>();
            services.AddSingleton<ISessionManager, SessionManager>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IRestaurantService, RestaurantService>();

            services.AddMediatR(opt =>
            {
                opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddDbContext<RestoratorDbContext>(opt =>
            {
                opt.UseSqlServer("Server=DESKTOP-F1TRK20\\SQLEXPRESS;Database=Restorator;TrustServerCertificate=true;Trusted_connection=true");
                //opt.UseSqlServer("Server=b2-225-002\\SQLEXPRESS;Database=Restorator;TrustServerCertificate=true;Trusted_connection=true");
            });

            return services;
        }

        public static IServiceCollection ConfigureViews(this IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();

            services.AddSingleton<MainWindowViewModel>();

            var manager = new DataTemplateManager()
                .RegisterDataTemplate<AuthenticationViewModel, AuthenticationPage>()
                .RegisterDataTemplate<SignInViewModel, SignInControl>()
                .RegisterDataTemplate<SignUpViewModel, SignUpControl>()
                .RegisterDataTemplate<RestaurantPreviewViewModel, RestaurantPreviewControl>()
                .RegisterDataTemplate<ReservationViewModel, ReservationPage>()
                .RegisterDataTemplate<RestaurantSearchViewModel, RestaurantSearchPage>();

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
                IEnumerable<Type> types = assembly
                    .GetTypes()
                    .Where(x =>
                        x.IsClass
                        && x.Namespace!.StartsWith(namespaceName, StringComparison.InvariantCultureIgnoreCase)
                    );

                foreach (Type? type in types)
                {
                    if (services.All(x => x.ServiceType != type))
                    {
                        if (type == typeof(ViewModelBase))
                        {
                            continue;
                        }

                        _ = services.AddTransient(type);
                    }
                }
            }

            return services;
        }
    }
}
