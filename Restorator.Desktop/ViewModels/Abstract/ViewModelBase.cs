using CommunityToolkit.Mvvm.ComponentModel;

namespace Restorator.Desktop.ViewModels.Abstract
{
    public partial class ViewModelBase : ObservableObject
    {
        public virtual Task Initialize()
        {
            return Task.CompletedTask;
        }
    }
}