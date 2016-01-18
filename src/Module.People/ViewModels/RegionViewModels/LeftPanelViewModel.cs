using System.Collections.Generic;
using Prism.Mvvm;

namespace Module.People.ViewModels
{
    public class LeftPanelViewModel : BindableBase
    {
        private List<string> _people;
        public List<string> People
        {
            get { return _people; }
            private set { SetProperty(ref this._people, value); }
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
    }
}
