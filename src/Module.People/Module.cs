using System;
using System.Collections.Generic;
using Common;
using Common.Interfaces;
using Microsoft.Practices.Unity;
using Module.People.ViewModels;
using Module.People.Views;
using People.Domain;
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
            Dictionary<Type, Type> viewViewModelBindings = CreateViewViewModelBindings();
            container.RegisterInstance<Dictionary<Type, Type>>(viewViewModelBindings);
            container.RegisterType<IValidationService, ValidationService>();

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

        private Dictionary<Type, Type> CreateViewViewModelBindings()
        {
            Dictionary<Type, Type> viewViewModelBindings = new Dictionary<Type, Type>();

            //Region views:
            viewViewModelBindings.Add(typeof(LeftPanelView), typeof(LeftPanelViewModel));
            viewViewModelBindings.Add(typeof(MainPanelView), typeof(MainPanelViewModel));

            //Other views:
            viewViewModelBindings.Add(typeof(ShowPersonView), typeof(PersonViewModel));
            viewViewModelBindings.Add(typeof(EditPersonView), typeof(EditPersonViewModel));

            return viewViewModelBindings;
        }
    }
}
