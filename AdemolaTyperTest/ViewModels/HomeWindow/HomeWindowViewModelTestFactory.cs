using System;
using System.Windows;
using AdemolaTyper.DataSources;
using AdemolaTyper.DesignData;
using AdemolaTyper.ViewModels;
using MVVMLib.MVVMHelpers;

namespace AdemolaTyperTest.ViewModels.HomeWindow
{
    public class HomeWindowViewModelTestFactory : IFactory
    {
        #region IFactory Members

        public object CreateViewModel(DependencyObject sender)
        {
            throw new NotImplementedException();
        }

        public object CreateViewModel()
        {
            var homeWindowVm = new HomeWindowViewModel();
            homeWindowVm.ServiceLocator.RegisterService<IOptionsDataSource>(new DesignTimeOptionsDataSource());
            homeWindowVm.ServiceLocator.RegisterService<IGameOneDataSource>(new GameOneDesignTimeDataSource());
            return homeWindowVm;
        }

        #endregion
    }
}