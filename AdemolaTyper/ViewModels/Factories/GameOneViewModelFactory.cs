using System;
using System.Windows;
using AdemolaTyper.DataSources;
using AdemolaTyper.Extensions;
using MVVMLib;
using MVVMLib.Helpers;
using MVVMLib.MVVMHelpers;

namespace AdemolaTyper.ViewModels.Factories
{
    public class GameOneViewModelFactory : IFactory
    {
        public object CreateViewModel()
        {
            GameOneViewModel gameOneViewModel = new GameOneViewModel(new HomeWindowViewModel());
            IGameOneDataSource gameDataSource;
            if(Designer.IsDesignMode)
            {
                gameDataSource = new DesignData.GameOneDataSource();
                gameOneViewModel.ServiceLocator.RegisterService<IGameOneDataSource>(gameDataSource);
                gameDataSource.GetGameData().each(x => gameOneViewModel.AddWord(x));
                return gameOneViewModel;
            }
        
            return null;
        }

        public object CreateViewModel(ViewModelBase parentViewModel)
        {
            throw new NotImplementedException();
        }

        public object CreateViewModel(DependencyObject sender)
        {
            return CreateViewModel();
        }
    }
}

