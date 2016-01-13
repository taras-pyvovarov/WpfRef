using System.Windows;
using Shell.Views;
using Prism.Modularity;
using Prism.Unity;
using Common.Interfaces;
using Microsoft.Practices.Unity;

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
            //var a = new LifetimeManager();
            //this.Container.RegisterInstance(typeof(IWindowService), "taras", new WindowService(), );
            this.Container.RegisterInstance<IWindowService>(new WindowService());

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
