using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AdemolaTyper.ViewModels;

namespace AdemolaTyper.Views
{
    /// <summary>
    /// Interaction logic for GameOneView.xaml
    /// </summary>
    public partial class GameOneView : UserControl
    {
        public GameOneView()
        {
            InitializeComponent();
        }

        private void GameOneView_TextInput(object sender, TextCompositionEventArgs e)
        {
            var gameOneView = sender as GameOneView;
            if(gameOneView == null) return;
            var gameOneViewModel = gameOneView.DataContext as GameOneViewModel;
            if (gameOneViewModel != null) gameOneViewModel.KeyPressReceivedCommand.Execute(e.Text);
        }
    }
}
