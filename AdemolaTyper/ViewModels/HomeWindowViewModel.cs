using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using AdemolaTyper.DataSources;
using AdemolaTyper.Extensions;
using AdemolaTyper.ViewModels.Factories;
using MVVMLib;
using MVVMLib.Commands;

namespace AdemolaTyper.ViewModels
{
    public class HomeWindowViewModel : ViewModelBase
    {
        private RelayCommand _loadGameOneCommand;
        private MenuViewModel _menuViewModel;
        private GameOneViewModel _gameOneViewModel;
        private WorkspaceViewModel _workspace;
        private ObservableCollection<WorkspaceViewModel> _workspaces;
        private RelayCommand _currentWordIsProcessed;
        private int _currentWordIndex;
        private int _wordsPerMinute;

        public HomeWindowViewModel()
        {
            _menuViewModel = new MenuViewModel();
            _menuViewModel.RequestClose += MenuViewModelRequestClose;
            _menuViewModel.RequestStartGameOne += MenuViewModel_RequestStartGameOne;    
            _menuViewModel.ServiceLocator.RegisterService(GetService<IOptionsDataSource>());

            
            //Set current workspace
            Workspaces.Add(_menuViewModel);
            _workspace = _menuViewModel;
        }

        private void MenuViewModel_RequestStartGameOne(object sender, EventArgs e)
        {
            LoadGameOneCommand.Execute(null);
        }

        public WorkspaceViewModel Workspace
        {
            get { return _workspace; }
            set
            {
                _workspace = value;

                OnPropertyChanged("Workspace");
            }
        }


        public int CurrentWordIndex
        {
            get { return _currentWordIndex; }
            set
            {
                _currentWordIndex = value;
                OnPropertyChanged("CurrentWordIndex");
            }
        }

        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (_workspaces == null)
                {
                    _workspaces = new ObservableCollection<WorkspaceViewModel>();
                }
                return _workspaces;
            }
        }

        public MenuViewModel MenuViewModel
        {
            get { return _menuViewModel; }
            set
            {
                _menuViewModel = value;
                OnPropertyChanged("Options");
            }
        }

        public ICommand LoadGameOneCommand
        {
            get
            {
                if (_loadGameOneCommand == null)
                {
                    _loadGameOneCommand = new RelayCommand(param => LoadGameOne());
                }
                return _loadGameOneCommand;
            }
        }

        public int WordsPerMinute
        {
            get { return _wordsPerMinute; }

            set
            {
                _wordsPerMinute = value;
                OnPropertyChanged("WordsPerMinute");
            }
        }

        private void MenuViewModelRequestClose(object sender, EventArgs e)
        {

        }

        private void LoadGameOne()
        {
            if (Workspace is GameOneViewModel) return;
            _gameOneViewModel = new GameOneViewModel(this);
            _gameOneViewModel.ServiceLocator.RegisterService(GetService<IGameOneDataSource>());

            GetService<IGameOneDataSource>().GetGameData().each(x => _gameOneViewModel.AddWord(x));
            _gameOneViewModel.SetFirstWord(_gameOneViewModel.Words.First());
            _gameOneViewModel.ProcessStart.Execute(null);
            Workspace = _gameOneViewModel;
        }

    }
}