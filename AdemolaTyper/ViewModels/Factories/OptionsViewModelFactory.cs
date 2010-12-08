using System.Windows;
using AdemolaTyper.DataSources;
using AdemolaTyper.Helpers;
using AdemolaTyper.MVVMHelpers;

namespace AdemolaTyper.ViewModels.Factories
{
    public class OptionsViewModelFactory : IFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            var optionsVm = new OptionsViewModel();
            if(Designer.IsDesignMode)
            {
                optionsVm.ServiceLocator.RegisterService<IOptionsDataSource>(new DesignData.DesignTimeOptionsDataSource());
            }
            else
            {
                optionsVm.ServiceLocator.RegisterService<IOptionsDataSource>(new OptionsDataSource());
            }
            return optionsVm;
        }
    }
}