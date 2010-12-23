using System.Windows;

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
            var homeWindow = new HomeWindow();
            homeWindow.Show();
        }
    }
}
