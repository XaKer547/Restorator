using Microsoft.Extensions.DependencyInjection;
using Restorator.Desktop.ViewModels.Abstract;
using System.Windows.Controls;

namespace Restorator.Desktop.Services
{
    public interface INavigationService
    {
        public void Navigate<T>() where T : ViewModelBase;
        public void SetNavigationControl(Frame frame);
    }

    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        private Frame _navigationControl;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Navigate<T>() where T : ViewModelBase
        {
            var item = _serviceProvider.GetRequiredService<T>();

            _navigationControl.Navigate(item);
        }

        public void SetNavigationControl(Frame navigationControl)
        {
            _navigationControl = navigationControl;
        }
    }
}
