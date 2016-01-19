using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell.ViewModels
{
    public class StatusPanelViewModel : BindableBase
    {
        private string _status;
        public string Status
        {
            get { return _status; }
            private set { SetProperty(ref this._status, value); }
        }

        public StatusPanelViewModel()
        {
            Status = "[Messages will appear here]";
        }
    }
}
