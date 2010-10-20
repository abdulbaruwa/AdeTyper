using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using AdemolaTyper.ViewModels;

namespace AdemolaTyper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Boolean ResizeInProcess;

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


        #region sizing events
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Resize_Init(object sender, MouseButtonEventArgs e)
        {
            var senderRect = sender as Rectangle;

            if (senderRect != null)
            {
                ResizeInProcess = true;

                senderRect.CaptureMouse();
            }
        }

        private void Resize_End(object sender, MouseButtonEventArgs e)
        {
            var senderRect = sender as Rectangle;

            if (senderRect != null)
            {
                ResizeInProcess = false;
                ;

                senderRect.ReleaseMouseCapture();
            }
        }

        private void Resizeing_Form(object sender, MouseEventArgs e)
        {
            if (ResizeInProcess)
            {
                var senderRect = sender as Rectangle;

                if (senderRect != null)
                {
                    double width = e.GetPosition(this).X;

                    double height = e.GetPosition(this).Y;

                    senderRect.CaptureMouse();

                    if (senderRect.Name == "ResizeWidth")
                    {
                        width += 5;

                        if (width > 0)

                            Width = width;
                    }

                    else if (senderRect.Name == "ResizeHeigth")
                    {
                        height += 5;

                        if (height > 0)

                            Height = height;
                    }
                }
            }
        }

        private void Form_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Form_Min(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Form_max_restore(object sender, RoutedEventArgs e)
        {
            var mainwin = new MainWindow();
            mainwin.Left = Left / 2;
            //mainwin.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            mainwin.Show();
            //WindowState = (WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            this.TypeSurface.Focus();
        }
    }
}