using System;
using System.Windows;
using AdemolaTyper.ViewModels.GameOne;
using MVVMLib;
using MVVMLib.Helpers;
using MVVMLib.MVVMHelpers;

namespace AdemolaTyper.ViewModels.Factories
{
    public class GameOneOverViewModelFactory : IFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            return CreateViewModel();
        }

        public object CreateViewModel()
        {
            GameOneOverViewModel gameOneViewModel = new GameOneOverViewModel();

            if (Designer.IsDesignMode)
            {
                gameOneViewModel.AdjustedScore = "21 Words Per Minute";
                gameOneViewModel.Score = "33 Words Per Minute";
                return gameOneViewModel;
            }

            return null;
        }

        public object CreateViewModel(ViewModelBase parentViewModel)
        {
            throw new NotImplementedException();
        }
    }
}