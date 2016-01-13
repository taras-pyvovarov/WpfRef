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
        public void ShowDialog(object viewModel)
        {
            Window window = new Window();

            DataTemplate dt = new DataTemplate();
            dt.DataType = viewModel.GetType();
            FrameworkElementFactory fef = new FrameworkElementFactory(typeof(ActionPanelView));
            dt.VisualTree = fef;
            DataTemplateKey dtKey = new DataTemplateKey(viewModel.GetType());
            window.Resources.Add(dtKey, dt);

            window.Content = viewModel;
            window.ShowDialog();
        }
    }
}
