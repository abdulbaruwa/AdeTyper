using System;
using MVVMLib;
using MVVMLib.Commands;
using System.Windows.Input;

namespace AdemolaTyper.ViewModels.GameOne
{
    public class GameOneOverViewModel : ViewModelBase
    {
        private string _adjustedScore;
        private RelayCommand _playAgain;
        private string _score;
        private RelayCommand _playNew;
        private bool _processCompleted;

        public event EventHandler RePlayCurrentGame;
        public event EventHandler PlayNewGame;

        public void InvokePlayNewGame()
        {
            EventHandler handler = PlayNewGame;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public void InvokeRePlayCurrentGame()
        {
            EventHandler handler = RePlayCurrentGame;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public string Score
        {
            get { return _score; }
            set
            {
                _score = value;
                OnPropertyChanged("Score");
            }
        }

        public string AdjustedScore
        {
            get { return _adjustedScore; }
            set
            {
                _adjustedScore = value;
                OnPropertyChanged("AdjustedScore");
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


        public ICommand PlayAgain
        {
            get
            {
                if (_playAgain == null)
                {
                    _playAgain = new RelayCommand(param => PlayCurrentGameAgain());
                }
                return _playAgain;
            }
        }

        public ICommand PlayNew
        {
            get
            {
                if(_playNew == null)
                {
                    _playNew = new RelayCommand(param => PlayANewGame());
                }
                return _playNew;
            }
        }

        private void PlayANewGame()
        {
            InvokePlayNewGame();
        }

        private void PlayCurrentGameAgain()
        {
            InvokeRePlayCurrentGame();
        }
    }
}