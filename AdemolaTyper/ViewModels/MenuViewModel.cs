using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AdemolaTyper.DataSources;
using AdemolaTyper.Extensions;
using MVVMLib.Commands;

namespace AdemolaTyper.ViewModels
{
    public class MenuViewModel : WorkspaceViewModel
    {
        private RelayCommand _closeCommand;
        private bool _editingUserName;
        private ObservableCollection<string> _levels = new ObservableCollection<string>();
        private ObservableCollection<TypeTest> _typeTests = new ObservableCollection<TypeTest>();
        private string _userName;
        private RelayCommand _playGameOneCommand;

        private void LoadData()
        {
            var dataSource = GetService<IOptionsDataSource>();
            _userName = dataSource.GetCurrentUser();
            dataSource.GetLevels().each(x => _levels.Add(x));
            dataSource.GetTypeTests().each(x => _typeTests.Add(x));
        }

        public bool EditingUserName
        {
            get
            {
                return _editingUserName;
            }
            set
            {
                _editingUserName = value;
                OnPropertyChanged("EditingUserName");
            }
        }

        public string UserName
        {
            get
            {
                if(string.IsNullOrEmpty(_userName))LoadData();
                return  _userName;
            }
            set
            {
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }

        public ObservableCollection<string> Levels
        {
            get { 
                
                if(_levels == null) LoadData();
                return _levels; 
            }
            set
            {
                _levels = value;
                OnPropertyChanged("Levels");
            }
        }

        public ObservableCollection<TypeTest> TypeTests
        {
            get
            {
                if(_typeTests == null)LoadData();
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

        public ICommand PlayGameOneCommand
        {
            get
            {
                if (_playGameOneCommand == null)
                {
                    _playGameOneCommand = new RelayCommand(param => PlayGameOne());
                }
                return _playGameOneCommand;

            }
        }

        private void PlayGameOne()
        {
            CloseDialog();
            this.InvokeRequestStartGameOne();
        }

        public event EventHandler RequestClose;
        public event EventHandler RequestStartGameOne;

        public void InvokeRequestClose()
        {
            EventHandler handler = RequestClose;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public void InvokeRequestStartGameOne()
        {
            EventHandler handler = RequestStartGameOne;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private void CloseDialog()
        {
            this.InvokeRequestClose();
        }

        public override string WorkpspaceName
        {
            get { return "Options"; }
        }
    }
}