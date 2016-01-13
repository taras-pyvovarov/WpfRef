using System;
using Common;
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

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(x => GetViewModelForView(x));
        }

        public void Initialize()
        {
            // View will be automatically injected to the region when the region is first displayed.
            this.regionManager.RegisterViewWithRegion(AppConstants.LogoRegion, () => this.container.Resolve<LogoView>());
            this.regionManager.RegisterViewWithRegion(AppConstants.LeftRegion, () => this.container.Resolve<LeftPanelView>());
            this.regionManager.RegisterViewWithRegion(AppConstants.MainRegion, () => this.container.Resolve<MainPanelView>());
            this.regionManager.RegisterViewWithRegion(AppConstants.StatusRegion, () => this.container.Resolve<BottomPanelView>());
        }

        private Type GetViewModelForView(Type viewType)
        {
            if (viewType == typeof(BottomPanelView))
                return typeof(StatusbarViewModel);
            if (viewType == typeof(LeftPanelView))
                return typeof(LeftPanelViewModel);
            if (viewType == typeof(MainPanelView))
                return typeof(MainPanelViewModel);
            return null;
        }
    }
}
