using Common;
using Microsoft.Practices.Unity;
using Module.Products.Views;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
            Dictionary<Type, Type> viewViewModelBindings = CreateViewViewModelBindings();
            container.RegisterInstance<Dictionary<Type, Type>>(viewViewModelBindings);
            container.RegisterType<ProductLeftView, ProductLeftView>("ProductLeftView");

            // View will be automatically injected to the region when the region is first displayed.
            this.regionManager.RegisterViewWithRegion(AppConstants.LeftRegion, () => this.container.Resolve<ProductLeftView>());
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
            //viewViewModelBindings.Add(typeof(LeftPanelView), typeof(LeftPanelViewModel));
            //viewViewModelBindings.Add(typeof(MainPanelView), typeof(MainPanelViewModel));

            return viewViewModelBindings;
        }
    }
}
