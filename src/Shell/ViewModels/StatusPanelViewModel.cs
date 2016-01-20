using Presentation.GlobalPubSubEvents;
using Prism.Events;
using Prism.Mvvm;

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

        public StatusPanelViewModel(IEventAggregator eventAggregator)
        {
            Status = "[Messages will appear here]";

            eventAggregator.GetEvent<UserActionHappenedEvent>().Subscribe(UserActionHappened);
        }

        private void UserActionHappened(string parameter)
        {
            Status = parameter;
        }
    }
}
