using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MVVMLib;
using MVVMLib.Commands;

namespace AdemolaTyper.ViewModels
{
    public class WordViewModel : ViewModelBase
    {
        private string _currentLetter;
        private ObservableCollection<TypeFaceViewModel> _letters;
        private int _wordHeight;
        private RelayCommand _wordTyped;
        private int _currentLetterIndex;
        private bool _isComplete;
        private GameOneViewModel _gameViewModel;
        private bool _isLastWord;
        private bool _startAnimation;

        public bool StartAnimation
        {
            get { return _startAnimation; }
            set
            {
                _startAnimation = value;
                OnPropertyChanged("StartAnimation");
            }
        }

        public bool IsLastWord
        {
            get { return _isLastWord; }
            set { _isLastWord = value; 
            OnPropertyChanged("IsLastWord");}
        }


         
        public WordViewModel(GameOneViewModel gameViewModel)
        {
            _gameViewModel = gameViewModel;
            _letters = new ObservableCollection<TypeFaceViewModel>();
        }

        public WordViewModel()
        {
            _letters = new ObservableCollection<TypeFaceViewModel>();
        }

        public ObservableCollection<TypeFaceViewModel> Letters
        {
            get { return _letters; }
            set
            {
                _letters = value;
                OnPropertyChanged("Letters");
            }
        }

        public int WordHeight
        {
            get { return _wordHeight; }
            set
            {
                _wordHeight = value;
                OnPropertyChanged("WordHeight");
            }
        }

        public string CurrentLetter
        {
            get { return _currentLetter; }
            set
            {
                _currentLetter = value;
                OnPropertyChanged("CurrentLetter");
            }
        }

        public ICommand WordTyped
        {
            get
            {
                if (_wordTyped == null)
                {
                    _wordTyped = new RelayCommand(param => WordTypeReceived(param));
                }
                return _wordTyped;
            }
        }

        public bool IsComplete
        {
            get { return _isComplete; }
            set { _isComplete = value; }
        }

        public GameOneViewModel GameViewModel
        {
            get { return _gameViewModel; }
            set { _gameViewModel = value; }
        }

        private void WordTypeReceived(object letterTyped)
        {
            CurrentLetter = letterTyped.ToString();
            if(Letters[_currentLetterIndex].Letter == Convert.ToChar(CurrentLetter))
            {
                Letters[_currentLetterIndex].IsTypedRight = System.Windows.Media.Brushes.CadetBlue;
            }
            else
            {
                Letters[_currentLetterIndex].IsTypedRight = System.Windows.Media.Brushes.OrangeRed;    
            }
            
            //Check we have received all the letters for this word - it is inmaterial if the were correct.
            IsComplete = (Letters.Count == _currentLetterIndex + 1);
            if(IsComplete)
            {
                InvokeWordProcessed();
                //_gameViewModel.CurrentWordIsProcessed.Execute(null);
            }
            else
            {
                _currentLetterIndex++;
            }
        }

        public event EventHandler WordProcessed;
        public void InvokeWordProcessed()
        {
            EventHandler handler = WordProcessed;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}