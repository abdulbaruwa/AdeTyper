using System;
using System.Collections.ObjectModel;
using System.Timers;
using System.Windows.Input;
using AdemolaTyper.Commands;

namespace AdemolaTyper.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private WordViewModel _currentWord;
        private int _currentWordIndex;
        private RelayCommand _currentWordIsProcessed;
        private RelayCommand _keyPressReceivedCommand;
        private bool _processCompleted;
        private RelayCommand _processStartCommand;
        private DateTime? _processStartTime;
        private string _typedLetter;

        private ObservableCollection<WordViewModel> _words;
        private int _wordsPerMinute;

        public MainViewModel()
        {
            if (_words == null)
            {
                _words = new ObservableCollection<WordViewModel>();
            }
        }

        public ObservableCollection<WordViewModel> Words
        {
            get { return _words; }
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

        public ICommand ProcessStart
        {
            get
            {
                if (_processStartCommand == null)
                {
                    _processStartCommand = new RelayCommand(param => Start());
                }
                return _processStartCommand;
            }
        }

        public ICommand CurrentWordIsProcessed
        {
            get
            {
                if (_currentWordIsProcessed == null)
                    _currentWordIsProcessed = new RelayCommand(param => CurrentWordIsProcessedAction());
                return _currentWordIsProcessed;
            }
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

      private  Timer _timer = new Timer();
      

        private void CurrentWordIsProcessedAction()
        {
            if (CurrentWordIndex != Words.Count - 1)
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
                CurrentWord = Words[CurrentWordIndex];
                CurrentWord.StartAnimation = true;
            }
        }

        public void AddWord(WordViewModel wordToAdd)
        {
            wordToAdd.MainViewModel = this;
            Words.Add(wordToAdd);
        }

        private void Start()
        {
            _processStartTime = DateTime.Now;
            ProcessCompleted = false;

            _timer.Elapsed += modifyWpm;
            _timer.Interval = 100;
            _timer.Start();
        }

        private void modifyWpm(object sender, ElapsedEventArgs e)
        {
            if (CurrentWordIndex != Words.Count - 1)
            {
                if (ProcessStartTime.HasValue && CurrentWordIndex > 0)
                {
                    var elapsedSeconds = new TimeSpan( DateTime.Now.Ticks - ((DateTime) ProcessStartTime).Ticks).TotalSeconds;
                    var time = DateTime.Now - ProcessStartTime;

                    var seconds = Convert.ToDecimal(elapsedSeconds);
                    if (seconds > 0)
                    {
                        //WordsPerMinute = Convert.ToInt16(60 / ( seconds / Convert.ToDecimal(CurrentWordIndex)));
                        var tempvalue = Convert.ToInt32(Convert.ToDecimal(CurrentWordIndex/2)/seconds*60);
                        if(tempvalue > 100) tempvalue = 100;
                        WordsPerMinute = tempvalue;
                    }                    
                }
            }
        }

        private void keyPressed(Object key)
        {
            
            if (ProcessCompleted) return;
            if(! ProcessStartTime.HasValue)
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
    }
}