using System;
using System.Collections.Generic;
using Common;
using Microsoft.Practices.Unity;
using Module.Products.Views;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;

namespace Module.Products
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
            container.RegisterType<LeftPanelView, LeftPanelView>("ProductLeftView");
            container.RegisterType<MainPanelView, MainPanelView>("ProductMainView");

            // View will be automatically injected to the region when the region is first displayed.
            this.regionManager.RegisterViewWithRegion(AppConstants.LeftRegion, () => this.container.Resolve<LeftPanelView>());
            this.regionManager.RegisterViewWithRegion(AppConstants.MainRegion, () => this.container.Resolve<MainPanelView>());
        }

        private Type GetViewModelForView(Type viewType)
        {
            var bindings = container.Resolve<Dictionary<Type, Type>>();
            var viewModelType = bindings[viewType];
            return viewModelType;
        }
    }
}
