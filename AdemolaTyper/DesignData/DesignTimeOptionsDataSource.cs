using System.Collections.Generic;
using System.Collections.ObjectModel;
using AdemolaTyper.DataSources;
using AdemolaTyper.ViewModels;

namespace AdemolaTyper.DesignData
{
    public class DesignTimeOptionsDataSource : IOptionsDataSource

    {
        public string GetCurrentUser()
        {
            return "Ademola Baruwa";
        }

        public IList<string> GetLevels()
        {
            var levels = new ObservableCollection<string>();
            levels.Add("Demo Level");
            levels.Add("Level One");
            levels.Add("Level Two");
            levels.Add("Level Three");
            levels.Add("Level Four");
            return levels;
        }

        public IList<TypeTest> GetTypeTests()
        {
            var typeTests = new ObservableCollection<TypeTest>();
            typeTests.Add(new TypeTest());
            return typeTests;
        }

    }
}