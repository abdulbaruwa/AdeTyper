using System;
using System.Windows.Input;
using AdemolaTyper.Commands;
using AdemolaTyper.ViewModels;
using System.Collections.ObjectModel;

namespace AdemolaTyper.DesignData
{
    public class DesignTimeOptionsViewModel : ViewModelBase
    {
        private RelayCommand _closeCommand;
        private bool _editingUserName;
        private ObservableCollection<string> _levels;
        private MainViewModel _mainViewModel;
        private ObservableCollection<TypeTest> _typeTests;
        private string _userName;

        public DesignTimeOptionsViewModel()
        {
            _mainViewModel = new MainViewModel();
            _userName = "Test User Name";
        }

        public bool EditingUserName
        {
            get {
                return _editingUserName; 
            }
            set
            {
                _editingUserName = value;
                OnPropertyChanged("TypeTest");
            }
        }

        public string UserName
        {
            get
            {
                _userName = "Ademola Baruwa";
                return _userName;
            }
            set
            {
                _userName = value;
                OnPropertyChanged("TypeTest");
            }
        }

        public ObservableCollection<string> Levels
        {
            get
            {
                if(_levels == null)
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
        }

        public ObservableCollection<TypeTest> TypeTests
        {
            get
            {
                _typeTests = new ObservableCollection<TypeTest>();
                return _typeTests;
            }
            set
            {
                _typeTests = value;
                OnPropertyChanged("TypeTest");
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(param => CloseDialog());
                }
                return _closeCommand;
            }
        }


        public event EventHandler RequestClose;

        public void InvokeRequestClose()
        {
            EventHandler handler = RequestClose;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private void CloseDialog()
        {
            this.InvokeRequestClose();
        }

    }
}