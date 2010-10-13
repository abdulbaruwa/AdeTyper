using System.Windows;
using System.Windows.Input;
using AdemolaTyper.ViewModels;

namespace AdemolaTyper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2 || e.SystemKey == Key.F2)
            {
                var mainwindow = sender as MainWindow;
                var mainwindowViewModel = mainwindow.DataContext as MainViewModel;
                mainwindowViewModel.Words.Clear();
                mainwindowViewModel.Words = DesignData.DesignTimeData.InitializeDummyData();
            }

            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && (e.Key == Key.S || e.SystemKey == Key.S))
            {
                MessageBox.Show("Ctrl + S has been pressed");
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            var mainwindow = sender as MainWindow;
            var mainwindowViewModel = mainwindow.DataContext as MainViewModel;    
        }

        private void Window_TextInput(object sender, TextCompositionEventArgs e)
        {
            var mainwindow = sender as MainWindow;
            var mainwindowViewModel = mainwindow.DataContext as MainViewModel;
            mainwindowViewModel.KeyPressReceivedCommand.Execute(e.Text);
        }
    }
}