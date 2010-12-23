using System;
using System.Linq;
using System.Windows;
using AdemolaTyper.DataSources;
using AdemolaTyper.DesignData;
using AdemolaTyper.ViewModels;
using MVVMLib;
using MVVMLib.MVVMHelpers;

namespace AdemolaTyperTest.ViewModels.GameOne
{
    public class GameOneViewModelTestFactory : IFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            throw new NotImplementedException();
        }

        public object CreateViewModel()
        {
            return CreateViewModel(new HomeWindowViewModel());
        }

        public object CreateViewModel(ViewModelBase parentViewModel)
        {
            var gameOneViewModel = new GameOneViewModel(parentViewModel as HomeWindowViewModel);
            gameOneViewModel.ServiceLocator.RegisterService<IGameOneDataSource>(new GameOneDesignTimeDataSource());
            gameOneViewModel.CurrentWordIndex = 0;
            gameOneViewModel.LoadData();
            gameOneViewModel.SetFirstWord(gameOneViewModel.Words.First());
            return gameOneViewModel;
        }
    }
}