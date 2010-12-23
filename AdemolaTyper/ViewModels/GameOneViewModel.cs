using System;
using System.Linq;
using System.Timers;
using AdemolaTyper.ViewModels.GameOne;
using MVVMLib.Commands;
using System.Windows.Input;
using AdemolaTyper.Extensions;
using AdemolaTyper.DataSources;
using System.Collections.ObjectModel;

namespace AdemolaTyper.ViewModels
{
    public class GameOneViewModel : WorkspaceViewModel
    {
        private readonly Timer _timer = new Timer();
        private WordViewModel _currentWord;

        private int _currentWordIndex;
        private RelayCommand _currentWordIsProcessed;
        private RelayCommand _keyPressReceivedCommand;
        private bool _processCompleted;
        private RelayCommand _processStartCommand;
        private DateTime? _processStartTime;
        private string _typedLetter;
        private ObservableCollection<WordViewModel> _words = new ObservableCollection<WordViewModel>();
        private int _wordsPerMinute;
        private GameOneOverViewModel _gameOneOverViewModel;

        public GameOneViewModel(HomeWindowViewModel homeWindowViewModel)
        {
            HomeViewModel = homeWindowViewModel;   
            _gameOneOverViewModel = new GameOneOverViewModel();
            _gameOneOverViewModel.RePlayCurrentGame += GameOneOverViewModel_RePlayCurrentGame;
            _gameOneOverViewModel.PlayNewGame += GameOneOverViewModel_PlayNewGame;
        }

        private void GameOneOverViewModel_PlayNewGame(object sender, EventArgs e)
        {
            ProcessCompleted = false;
            _gameOneOverViewModel.ProcessCompleted = false;
            Words.Clear();
            LoadData();
        }

        private void GameOneOverViewModel_RePlayCurrentGame(object sender, EventArgs e)
        {
            ProcessCompleted = false;
            _gameOneOverViewModel.ProcessCompleted = false;
        }

        public DateTime? ProcessStartTime
        {
            get { return _processStartTime; }
            set
            {
                _processStartTime = value;
                OnPropertyChanged("ProcessStartTime");
            }
        }

        public override string WorkpspaceName
        {
            get { return "Game One"; }
        }

        public ObservableCollection<WordViewModel> Words
        {
            get
            {
                if (_words == null) LoadData();
                return _words;
            }
            set
            {
                _words = value;
                OnPropertyChanged("Words");
            }
        }

        public string TypedLetter
        {
            get { return _typedLetter; }
            set
            {
                _typedLetter = value;
                OnPropertyChanged("TypedLetter");
            }
        }

        public ICommand KeyPressReceivedCommand
        {
            get
            {
                if (_keyPressReceivedCommand == null)
                {
                    _keyPressReceivedCommand = new RelayCommand(param => keyPressed(param));
                }
                return _keyPressReceivedCommand;
            }
        }

        public WordViewModel CurrentWord
        {
            get
            {
                if (_currentWord == null)
                {
                    _currentWord = Words[_currentWordIndex];
                }
                return _currentWord;
            }
            set
            {
                _currentWord = value;
                OnPropertyChanged("CurrentWord");
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

        public bool ProcessCompleted
        {
            get { return _processCompleted; }
            set
            {
                _processCompleted = value;
                OnPropertyChanged("ProcessCompleted");
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
        
        public HomeWindowViewModel HomeViewModel { get; set; }

        public ICommand ProcessStart
        {
            get
            {
                if (_processStartCommand == null)
                {
                    _processStartCommand = new RelayCommand(param => StartGame());
                }
                return _processStartCommand;
            }
        }

        public void LoadData()
        {
            var gameDataSource = GetService<IGameOneDataSource>();
            gameDataSource.GetGameData(this).each(x => Words.Add(x));
            //CurrentWord = Words.First();
            //WordsPerMinute = 0;
            CurrentWordIndex = 0;
            SetFirstWord(Words.First());
            StartGame();
        }

        private void keyPressed(Object key)
        {
            if (ProcessCompleted) return;
            if (!ProcessStartTime.HasValue)
            {
                ProcessStartTime = DateTime.Now;
            }

            TypedLetter = key.ToString();
            CurrentWord.WordTyped.Execute(key);
            if (CurrentWordIndex == Words.Count - 1 && CurrentWord.IsComplete)
            {
                ProcessCompleted = true;
                GameOneOver.AdjustedScore = WordsPerMinute.ToString();
                GameOneOver.Score = WordsPerMinute.ToString();
                GameOneOver.ProcessCompleted = true;
                ProcessStartTime = null;
            }
        }

        public GameOneOverViewModel GameOneOver
        {
            get { return _gameOneOverViewModel; }
            set
            {
                _gameOneOverViewModel = value;
            }
        }

        public void AddWords(ObservableCollection<WordViewModel> initializeDummyData)
        {
            foreach (WordViewModel wordViewModel in initializeDummyData)
            {
                AddWord(wordViewModel);
            }
        }

        public void AddWord(WordViewModel wordToAdd)
        {
            wordToAdd.GameViewModel = this;
            _words.Add(wordToAdd);
        }

        private void StartGame()
        {
            _processStartTime = DateTime.Now;
            ProcessCompleted = false;

            _timer.Elapsed += ModifyWpm;
            _timer.Interval = 100;
            _timer.Start();
        }

        private void CurrentWordProcessed()
        {
            if (CurrentWordIndex != _words.Count - 1)
            {
                CurrentWordIndex++;
                CurrentWord.StartAnimation = false;
                CurrentWord.WordProcessed -= CurrentWordWordProcessed;
                CurrentWord = _words[CurrentWordIndex];
                CurrentWord.WordProcessed += CurrentWordWordProcessed;
                CurrentWord.StartAnimation = true;
            }
        }

        private void CurrentWordWordProcessed(object sender, EventArgs e)
        {
            CurrentWordProcessed();
        }

        private void ModifyWpm(object sender, ElapsedEventArgs e)
        {
            if (CurrentWordIndex != _words.Count - 1)
            {
                if (ProcessStartTime.HasValue && CurrentWordIndex > 0)
                {
                    double elapsedSeconds =
                        new TimeSpan(DateTime.Now.Ticks - ((DateTime) ProcessStartTime).Ticks).TotalSeconds;
                    TimeSpan? time = DateTime.Now - ProcessStartTime;

                    decimal seconds = Convert.ToDecimal(elapsedSeconds);
                    if (seconds > 0)
                    {
                        //WordsPerMinute = Convert.ToInt16(60 / ( seconds / Convert.ToDecimal(CurrentWordIndex)));
                        int tempvalue = Convert.ToInt32(Convert.ToDecimal(CurrentWordIndex/2)/seconds*60);
                        if (tempvalue > 100) tempvalue = 100;
                        WordsPerMinute = tempvalue;
                        HomeViewModel.WordsPerMinute = WordsPerMinute;
                    }
                }
            }
        }

        public void SetFirstWord(WordViewModel wordViewModel)
        {
            if (_currentWord != null) _currentWord.WordProcessed -= CurrentWordWordProcessed;
            _currentWord = wordViewModel;
            _currentWord.WordProcessed += CurrentWordWordProcessed;
        }
    }
}