using System.Collections.Generic;
using AdemolaTyper.ViewModels;

namespace AdemolaTyper.DataSources
{
    public interface IGameOneDataSource
    {
        IList<WordViewModel> GetGameData();
        IList<WordViewModel> GetGameData(GameOneViewModel gameViewModel);
    }
}