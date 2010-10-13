using System;
using System.Windows;
using AdemolaTyper.DesignData;
using AdemolaTyper.ViewModels;
using System.Collections.ObjectModel;

namespace AdemolaTyper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            LoadModelView();
        }

        private void LoadModelView()
        {
            var mainWindow = new MainWindow();
            var viewModel = new MainViewModel();
            viewModel.AddWords(DesignTimeData.InitializeDummyData());
            mainWindow.DataContext = viewModel;
            mainWindow.Show();
        }
    }
}
