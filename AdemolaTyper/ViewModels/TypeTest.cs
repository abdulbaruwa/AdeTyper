namespace AdemolaTyper.ViewModels
{
    public class TypeTest
    {
        private string _testName;
        private TypeTestLevel _typeTestLevel;
        private string _typeTest;

        public string TestName
        {
            get { return _testName; }
            set
            {
                _testName = value;
            }
        }

        public TypeTestLevel TypeTestLevel
        {
            get { return _typeTestLevel; }
            set { _typeTestLevel = value; }
        }

        public string TypeTest1
        {
            get { return _typeTest; }
            set { _typeTest = value; }
        }

        public int TargetWordsPerMinute
        {
            get { return _targetWordsPerMinute; }
            set { _targetWordsPerMinute = value; }
        }

        private int _targetWordsPerMinute;
    }
}