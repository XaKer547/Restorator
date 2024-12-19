using Microsoft.Extensions.DependencyInjection;
using Restorator.Desktop.ViewModels.Abstract;
using System.Windows.Controls;

namespace Restorator.Desktop.Services
{
    public interface INavigationService
    {
        void Navigate<TViewModel, TData>(TData data) where TViewModel : ViewModelBase, IValueHandler<TData>;
        void Navigate<T>() where T : ViewModelBase;
        void SetNavigationControl(Frame frame);
    }

    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        private Frame _navigationControl;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Navigate<TViewModel>() where TViewModel : ViewModelBase
        {
            var item = _serviceProvider.GetRequiredService<TViewModel>();

            _navigationControl.Navigate(item);
        }

        public void Navigate<TViewModel, TData>(TData data) where TViewModel : ViewModelBase, IValueHandler<TData>
        {
            //var item = _serviceProvider.GetRequiredService<TViewModel>();

            //var wrapperType = typeof(ViewModelBase<>).MakeGenericType(requestType, typeof(TResponse));

            //item.Initialize(data);

            //_navigationControl.Navigate(item);
        }


        public void SetNavigationControl(Frame navigationControl)
        {
            _navigationControl = navigationControl;
        }
    }
}
