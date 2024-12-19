namespace Restorator.Desktop.ViewModels.Abstract
{
    public interface IValueHandler<TData>
    {
        void Handle(TData data);
    }
}
