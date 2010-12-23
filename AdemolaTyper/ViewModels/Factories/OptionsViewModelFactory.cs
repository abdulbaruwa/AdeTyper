using System.Windows;
using AdemolaTyper.DataSources;
using MVVMLib.Helpers;
using MVVMLib.MVVMHelpers;

namespace AdemolaTyper.ViewModels.Factories
{
    public class OptionsViewModelFactory : IFactory
    {
        public object CreateViewModel()
        {
            //I am only interested in a Design time implementation. 
            var optionsVm = new MenuViewModel();
            if(Designer.IsDesignMode)
            {
                optionsVm.ServiceLocator.RegisterService<IOptionsDataSource>(new DesignData.DesignTimeOptionsDataSource());
                return optionsVm;
            }
            return null;
        }

        public object CreateViewModel(DependencyObject sender)
        {
            return CreateViewModel();
        }
    }
}