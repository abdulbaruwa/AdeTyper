using System.Windows;
using System.Windows.Controls;

namespace AdemolaTyper.Views
{
    /// <summary>
    /// Interaction logic for TypeFaceView.xaml
    /// </summary>
    public partial class TypeFaceView : TextBlock
    {
        public static readonly DependencyProperty TypeFaceCharProperty = DependencyProperty.Register("TypeFaceChar",
                                                                                                     typeof (string),
                                                                                                     typeof (
                                                                                                         TypeFaceView),
                                                                                                     new FrameworkPropertyMetadata
                                                                                                         (OnPropertyChange,
                                                                                                          OnCoerceProperty));

        public TypeFaceView()
        {
            InitializeComponent();
        }

        public string TypeFaceChar
        {
            get { return (string) GetValue(TypeFaceCharProperty); }
            set { SetValue(TypeFaceCharProperty, value); }
        }

        private static object OnCoerceProperty(DependencyObject d, object basevalue)
        {
            string newValue = basevalue.ToString();
            if (newValue.Length > 0) return newValue.Substring(0, 1);
            return newValue;
        }

        private static void OnPropertyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TypeFaceView) d).OnPropertyChange(e);
        }

        private void OnPropertyChange(DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ItemText.Text = dependencyPropertyChangedEventArgs.NewValue.ToString();
        }
    }

}