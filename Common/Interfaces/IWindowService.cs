using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IWindowService
    {
        void ShowDialog(object viewModel, Dictionary<Type, Type> viewViewModelBindings);
    }
}
