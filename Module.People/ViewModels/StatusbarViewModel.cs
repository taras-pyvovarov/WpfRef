using Prism.Mvvm;

namespace Module.People.ViewModels
{
    public class StatusbarViewModel : BindableBase
    {
        private string _status;

        public string Status
        {
            get { return _status; }
            private set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public StatusbarViewModel()
        {
            Status = Properties.Resources.Statusbar_DefaultText;
        }
    }
}
