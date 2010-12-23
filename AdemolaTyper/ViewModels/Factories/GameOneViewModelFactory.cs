using System.Windows;
using AdemolaTyper.DataSources;
using AdemolaTyper.Extensions;
using MVVMLib.Helpers;
using MVVMLib.MVVMHelpers;

namespace AdemolaTyper.ViewModels.Factories
{
    public class GameOneViewModelFactory : IFactory
    {
        public object CreateViewModel()
        {
            GameOneViewModel gameOneViewModel = new GameOneViewModel();
            IGameOneDataSource gameDataSource;
            if(Designer.IsDesignMode)
            {
                gameDataSource = new DesignData.GameOneDesignTimeDataSource();
                gameOneViewModel.ServiceLocator.RegisterService<IGameOneDataSource>(gameDataSource);
                gameDataSource.GetGameData().each(x => gameOneViewModel.AddWord(x));
                return gameOneViewModel;
            }
        
            return null;
        }

        public object CreateViewModel(DependencyObject sender)
        {
            return CreateViewModel();
        }
    }
}

