using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AdemolaTyper.ViewModels
{
    public class OptionsViewModel : ViewModelBase
    {
        private bool _editingUserName;
        private IList<string> _levels;
        private ObservableCollection<TypeTest> _typeTests;
        private string _userName;

        public bool EditingUserName
        {
            get { return _editingUserName; }
            set
            {
                _editingUserName = value;
                OnPropertyChanged("TypeTest");
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged("TypeTest");
            }
        }

        public IList<string> Levels
        {
            get { return _levels; }
            set
            {
                _levels = value;
                OnPropertyChanged("TypeTest");
            }
        }

        public ObservableCollection<TypeTest> TypeTests
        {
            get { return _typeTests; }
            set
            {
                _typeTests = value;
                OnPropertyChanged("TypeTest");
            }
        }
    }
}