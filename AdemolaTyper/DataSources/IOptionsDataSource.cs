using System.Collections.Generic;
using AdemolaTyper.ViewModels;

namespace AdemolaTyper.DataSources
{
    public interface IOptionsDataSource
    {
        string GetCurrentUser();
        IList<string> GetLevels();
        IList<TypeTest> GetTypeTests();
    }
}