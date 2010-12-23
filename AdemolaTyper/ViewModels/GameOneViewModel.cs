using System;
using System.Timers;
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

        public HomeWindowViewModel MainViewModel { get; set; }

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
            _words = new ObservableCollection<WordViewModel>();
            gameDataSource.GetGameData(this).each(x => _words.Add(x));
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
                ProcessStartTime = null;
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

            _timer.Elapsed += modifyWpm;
            _timer.Interval = 100;
            _timer.Start();
        }

        //public ICommand CurrentWordIsProcessed
        //{
        //    get
        //    {
        //        if (_currentWordIsProcessed == null)
        //        {
        //            _currentWordIsProcessed = new RelayCommand(param => CurrentWordProcessed());
        //        }
        //        return _currentWordIsProcessed;
        //    }
        //}

        private void CurrentWordProcessed()
        {
            if (CurrentWordIndex != _words.Count - 1)
            {
                //if (ProcessStartTime.HasValue && CurrentWordIndex > 0)
                //{
                //    decimal seconds = DateTime.Now.Subtract(((DateTime) ProcessStartTime)).Seconds;
                //    if (seconds > 0)
                //    {
                //        //WordsPerMinute = Convert.ToInt16(60 / ( seconds / Convert.ToDecimal(CurrentWordIndex)));
                //        WordsPerMinute = Convert.ToInt16(Convert.ToDecimal(CurrentWordIndex)/seconds*60);

                //    }                    
                //}
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

        private void modifyWpm(object sender, ElapsedEventArgs e)
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