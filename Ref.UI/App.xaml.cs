using System.Windows;

namespace AppUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //Running UI composition bootstrapper:
            UICompositionBootstrapper bootstrapper = new UICompositionBootstrapper();
            bootstrapper.Run();
        }
    }
}
