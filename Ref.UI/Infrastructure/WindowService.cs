using System;
using System.Collections.Generic;
using System.Windows;
using Common.Interfaces;
using Microsoft.Practices.Unity;

namespace Shell
{
    public class WindowService : IWindowService
    {
        private Window owner;
        private Dictionary<Type, Type> viewViewModelBindings;

        public WindowService(IUnityContainer container)
        {
            viewViewModelBindings = container.Resolve<Dictionary<Type, Type>>();
            owner = container.Resolve<Window>();
        }

        public void ShowDialog(object viewModel)
        {
            Window window = new Window();
            AddWindowBindings(window, viewViewModelBindings);
            window.SizeToContent = SizeToContent.WidthAndHeight;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.Owner = owner;

            window.Content = viewModel;
            window.ShowDialog();
        }

        private void AddWindowBindings(Window window, Dictionary<Type, Type> viewViewModelBindings)
        {
            foreach (var singleBinding in viewViewModelBindings)
            {
                DataTemplate dt = new DataTemplate();
                dt.DataType = singleBinding.Value;
                FrameworkElementFactory fef = new FrameworkElementFactory(singleBinding.Key);
                dt.VisualTree = fef;
                DataTemplateKey dtKey = new DataTemplateKey(singleBinding.Value);
                window.Resources.Add(dtKey, dt);
            }
        }
    }
}
