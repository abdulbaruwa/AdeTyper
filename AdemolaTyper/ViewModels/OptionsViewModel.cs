using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AdemolaTyper.Commands;

namespace AdemolaTyper.ViewModels
{
    public class OptionsViewModel : ViewModelBase
    {
        private RelayCommand _closeCommand;
        private bool _editingUserName;
        private IList<string> _levels;
        private MainViewModel _mainViewModel;
        private ObservableCollection<TypeTest> _typeTests;
        private string _userName;

        public OptionsViewModel(MainViewModel mainViewModel)
        {
            if (mainViewModel == null) throw new ArgumentException("mainViewModel");
            _mainViewModel = mainViewModel;
        }


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