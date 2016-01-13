using Prism.Mvvm;

namespace Module.People.ViewModels
{
    public class StatusbarViewModel : BindableBase
    {
        private string _status;
        public string Status
        {
            get { return _status; }
            private set { SetProperty(ref this._status, value); }
        }

        public StatusbarViewModel()
        {
            Status = Properties.Resources.Statusbar_DefaultText;
        }
    }
}
