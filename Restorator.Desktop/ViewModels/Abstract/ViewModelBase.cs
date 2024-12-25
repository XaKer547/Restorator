using CommunityToolkit.Mvvm.ComponentModel;

namespace Restorator.Desktop.ViewModels.Abstract
{
    public abstract partial class ViewModelBase : ObservableObject
    {
        [ObservableProperty]
        public bool initialized = false;
        protected bool CanInitialize => !Initialized;
    }
}