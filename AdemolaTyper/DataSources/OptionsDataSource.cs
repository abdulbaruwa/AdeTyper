using System;
using System.Collections.Generic;
using AdemolaTyper.ViewModels;

namespace AdemolaTyper.DataSources
{
    public class OptionsDataSource : IOptionsDataSource
    {
        public string GetCurrentUser()
        {
            return "Adedayo Baruwa";
        }

        public IList<string> GetLevels()
        {
            return new List<string>();
        }

        public IList<TypeTest> GetTypeTests()
        {
            return new List<TypeTest>();
        }
    }
}