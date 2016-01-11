using Microsoft.Practices.Unity;
using Module.People.ViewModels;
using Module.People.Views;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;

namespace Module.People
{
    public class Module : IModule
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        public Module(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;

            //container.RegisterType<LeftPanelViewModel>();
            //ViewModelLocationProvider.SetDefaultViewModelFactory((x) =>
            //{
            //    return container.Resolve(x);
            //});   
        }

        public void Initialize()
        {
            // View will be automatically injected to the region when the region is first displayed.
            this.regionManager.RegisterViewWithRegion("LogoRegion", () => this.container.Resolve<LogoView>());
            this.regionManager.RegisterViewWithRegion("LeftRegion", () => this.container.Resolve<LeftPanelView>());
            this.regionManager.RegisterViewWithRegion("MainRegion", () => this.container.Resolve<MainPanelView>());
            this.regionManager.RegisterViewWithRegion("StatusRegion", () => this.container.Resolve<BottomPanelView>());
        }
    }
}
