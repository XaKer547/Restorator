using Microsoft.Extensions.DependencyInjection;
using Restorator.Desktop.Extensions;
using Restorator.Desktop.Infrastructure;
using Restorator.Desktop.Views;
using System.Windows;

namespace Restorator.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private readonly IServiceProvider _serviceProvider;
        public App()
        {
            var services = new ServiceCollection()
                .Configure();

            _serviceProvider = services.BuildServiceProvider();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            _serviceProvider.GetRequiredService<MainWindow>()
                .Show();
        }
    }
}
