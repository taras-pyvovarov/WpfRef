using System;
using Common;
using Microsoft.Practices.Unity;
using Module.People.ViewModels;
using Module.People.Views;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;

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
            Dictionary<Type, Type> viewViewModelBindings = CreateViewViewModelBindings();
            container.RegisterInstance<Dictionary<Type, Type>>(viewViewModelBindings);

            // View will be automatically injected to the region when the region is first displayed.
            this.regionManager.RegisterViewWithRegion(AppConstants.LogoRegion, () => this.container.Resolve<LogoView>());
            this.regionManager.RegisterViewWithRegion(AppConstants.LeftRegion, () => this.container.Resolve<LeftPanelView>());
            this.regionManager.RegisterViewWithRegion(AppConstants.MainRegion, () => this.container.Resolve<MainPanelView>());
            this.regionManager.RegisterViewWithRegion(AppConstants.StatusRegion, () => this.container.Resolve<BottomPanelView>());
        }

        private Type GetViewModelForView(Type viewType)
        {
            var bindings = container.Resolve<Dictionary<Type, Type>>();
            var viewModelType = bindings[viewType];
            return viewModelType;
        }

        private Dictionary<Type, Type> CreateViewViewModelBindings()
        {
            Dictionary<Type, Type> viewViewModelBindings = new Dictionary<Type, Type>();

            //Region views:
            viewViewModelBindings.Add(typeof(BottomPanelView), typeof(StatusbarViewModel));
            viewViewModelBindings.Add(typeof(LeftPanelView), typeof(LeftPanelViewModel));
            viewViewModelBindings.Add(typeof(MainPanelView), typeof(MainPanelViewModel));

            //Other views:
            viewViewModelBindings.Add(typeof(ActionPanelView), typeof(ActionPanelViewModel));

            return viewViewModelBindings;
        }
    }
}
