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
            moduleCatalog.AddModule(new ModuleInfo("PeopleModule", typeof(Module.People.Module).AssemblyQualifiedName));
            moduleCatalog.AddModule(new ModuleInfo("ProductsModule", typeof(Module.Products.Module).AssemblyQualifiedName));
        }

        protected override DependencyObject CreateShell()
        {
            // Use the container to create an instance of the shell.
            ShellWindow view = this.Container.TryResolve<ShellWindow>();
            return view;
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            App.Current.MainWindow = (Window)this.Shell;

            this.Container.RegisterType<IWindowService, WindowService>();
            this.Container.RegisterInstance<Window>((Window)this.Shell);

            App.Current.MainWindow.Show();
        }
    }
}
