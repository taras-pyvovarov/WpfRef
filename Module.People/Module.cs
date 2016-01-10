using Microsoft.Practices.Unity;
using Module.People.Views;
using Prism.Modularity;
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
        }

        public void Initialize()
        {
            // Register the EmployeeDataService concrete type with the container.
            // Change this to swap in another data service implementation.
            //this.container.RegisterType<IEmployeeDataService, EmployeeDataService>();

            // View will be automatically injected to the region when the region is first displayed.
            this.regionManager.RegisterViewWithRegion("LogoRegion", () => this.container.Resolve<LogoView>());
            //this.regionManager.RegisterViewWithRegion("LeftRegion", () => this.container.Resolve<EmployeeDetailsView>());
            //this.regionManager.RegisterViewWithRegion("MainRegion", () => this.container.Resolve<EmployeeDetailsView>());
            //this.regionManager.RegisterViewWithRegion("StatusRegion", () => this.container.Resolve<EmployeeProjectsView>());
        }
    }
}
