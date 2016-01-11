using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.People.ViewModels
{
    public class LeftPanelViewModel : INotifyPropertyChanged
    {
        private List<string> _people;

        public List<string> People
        {
            get { return _people; }
            private set
            {
                _people = value;
                RaisePropertyChanged("People");
            }
        }

        public LeftPanelViewModel()
        {
            var people = new List<string>();
            people.Add("People1");
            people.Add("People2");
            people.Add("People3");
            people.Add("People4");
            people.Add("People5");

            People = people;
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
