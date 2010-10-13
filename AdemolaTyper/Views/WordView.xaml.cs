using System.Windows;
using System.Windows.Controls;

namespace AdemolaTyper.Views
{
    /// <summary>
    /// Interaction logic for WordView.xaml
    /// </summary>
    public partial class WordView : StackPanel
    {
        private Size lastRenderedSize;
        private Size lastBounds;

        public WordView()
        {
            InitializeComponent();
        }

    }
}