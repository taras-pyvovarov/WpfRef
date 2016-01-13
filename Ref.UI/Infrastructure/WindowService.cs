using Common.Interfaces;
using Module.People.ViewModels;
using Module.People.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Shell
{
    public class WindowService : IWindowService
    {
        public void ShowDialog(object viewModel, Dictionary<Type, Type> viewViewModelBindings)
        {
            Window window = new Window();
            window.SizeToContent = SizeToContent.WidthAndHeight;

            foreach (var singleBinding in viewViewModelBindings)
            {
                DataTemplate dt = new DataTemplate();
                dt.DataType = singleBinding.Value;
                FrameworkElementFactory fef = new FrameworkElementFactory(singleBinding.Key);
                dt.VisualTree = fef;
                DataTemplateKey dtKey = new DataTemplateKey(singleBinding.Value);
                window.Resources.Add(dtKey, dt);
            }

            window.Content = viewModel;
            window.ShowDialog();
        }
    }
}
