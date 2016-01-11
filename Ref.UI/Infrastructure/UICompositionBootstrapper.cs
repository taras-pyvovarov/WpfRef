using System.Windows;
using Shell.Views;
using Prism.Modularity;
using Prism.Unity;

namespace Shell
{
    internal class UICompositionBootstrapper : UnityBootstrapper
    {
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(Module.People.Module));
        }

        protected override DependencyObject CreateShell()
        {
            // Use the container to create an instance of the shell.
            ShellView view = this.Container.TryResolve<ShellView>();
            return view;
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            App.Current.MainWindow = (Window)this.Shell;
            App.Current.MainWindow.Show();
        }
    }
}
