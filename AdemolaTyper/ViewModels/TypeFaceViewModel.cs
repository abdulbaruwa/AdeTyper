namespace AdemolaTyper.ViewModels
{
    public class TypeFaceViewModel : ViewModelBase
    {
        private int _fontSize;
        private System.Windows.Media.Brush _isTypedRight = System.Windows.Media.Brushes.Black;
        private char _letter;

        public TypeFaceViewModel()
        {
        }

        public TypeFaceViewModel(char letter, int fontSize)
        {
            _letter = letter;
            _fontSize = fontSize;
        }

        public char Letter
        {
            get { return _letter; }
            set
            {
                _letter = value;
                OnPropertyChanged("Letter");
            }
        }

        public int TypeFaceFontSize
        {
            get { return _fontSize; }
            set
            {
                _fontSize = value;
                OnPropertyChanged("TypeFaceFontSize");
            }
        }

        public System.Windows.Media.Brush IsTypedRight
        {
            get { return _isTypedRight; }
            set
            {
                _isTypedRight = value;
                OnPropertyChanged("IsTypedRight");
            }
        }


    }
}