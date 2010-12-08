using System.Windows;

namespace AdemolaTyper.MVVMHelpers
{
    public interface IFactory
    {
        object CreateViewModel(DependencyObject sender);
    }
}