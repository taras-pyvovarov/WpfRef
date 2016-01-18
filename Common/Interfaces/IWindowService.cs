using System;
using System.Collections.Generic;

namespace Common.Interfaces
{
    public interface IWindowService
    {
        bool? ShowDialog(object viewModel);
    }
}
