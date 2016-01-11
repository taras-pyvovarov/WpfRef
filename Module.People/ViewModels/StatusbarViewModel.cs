using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.People.ViewModels
{
    public class StatusbarViewModel : INotifyPropertyChanged
    {
        private string _status;

        public string Status
        {
            get { return _status; }
            private set
            {
                _status = value;
                RaisePropertyChanged("Status");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propName)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
