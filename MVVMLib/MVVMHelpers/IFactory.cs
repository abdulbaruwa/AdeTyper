using System.Windows;

namespace MVVMLib.MVVMHelpers
{
    public interface IFactory
    {
        object CreateViewModel(DependencyObject sender);
        object CreateViewModel();
        object CreateViewModel(ViewModelBase parentViewModel);
    }
}