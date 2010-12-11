using System.Collections.ObjectModel;
using MVVMLib;

namespace AdemolaTyper.ViewModels
{
    public class Tester : ViewModelBase
    {
        private ObservableCollection<string> _levels;

        public ObservableCollection<string> Levels
        {
            get
            {
                if (_levels == null)
                {
                    LoadLevels();
                }
                return _levels;
            }
            set
            {
                _levels = value;
                OnPropertyChanged("TypeTest");
            }
        }

        private void LoadLevels()
        {
            _levels = new ObservableCollection<string>();
            _levels.Add("Demo Level");
            _levels.Add("Level One");
            _levels.Add("Level Two");
            _levels.Add("Level Three");
            _levels.Add("Level Four");
            _levels.Add("Level Five");
            _levels.Add("Level Six");
        }
    }
}