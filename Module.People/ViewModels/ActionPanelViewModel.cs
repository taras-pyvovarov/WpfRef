using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.People.ViewModels
{
    public class ActionPanelViewModel : BindableBase
    {
        private string _test;
        public string Test
        {
            get { return _test; }
            private set { SetProperty(ref this._test, value); }
        }

        public ActionPanelViewModel()
        {
            Test = "hello";
        }
    }
}
