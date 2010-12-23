using System;
using System.Windows;
using AdemolaTyper.DataSources;
using AdemolaTyper.DesignData;
using MVVMLib.Helpers;
using MVVMLib.MVVMHelpers;

namespace AdemolaTyper.ViewModels.Factories
{
    public class HomeWindowViewModelFactory : IFactory
    {
        public object CreateViewModel()
        {
            var homeWindowVm = new HomeWindowViewModel();
            if(Designer.IsDesignMode)
            {
                homeWindowVm.ServiceLocator.RegisterService<IOptionsDataSource>(new DesignTimeOptionsDataSource());
                homeWindowVm.ServiceLocator.RegisterService<IGameOneDataSource>(new GameOneDesignTimeDataSource());
            }
            else
            {
                homeWindowVm.ServiceLocator.RegisterService<IOptionsDataSource>(new OptionsDataSource());
                homeWindowVm.ServiceLocator.RegisterService<IGameOneDataSource>(new GameOneDesignTimeDataSource ());
            }
            return homeWindowVm;
        }

        public object CreateViewModel(DependencyObject sender)
        {
            return CreateViewModel();
        }
    }
}