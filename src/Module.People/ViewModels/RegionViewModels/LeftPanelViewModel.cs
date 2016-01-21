using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using People.Domain;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Module.People.PubSubEvents;
using Presentation.GlobalPubSubEvents;
using System.Linq;

namespace Module.People.ViewModels
{
    public class LeftPanelViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;

        private ObservableCollection<PersonListItemViewModel> _people;
        public ObservableCollection<PersonListItemViewModel> People
        {
            get { return _people; }
            private set { SetProperty(ref this._people, value); }
        }

        private PersonListItemViewModel _selectedPerson;
        public PersonListItemViewModel SelectedPerson
        {
            get { return _selectedPerson; }
            set 
            { 
                SetProperty(ref this._selectedPerson, value);
                _eventAggregator.GetEvent<SelectedPersonChangedEvent>().Publish(_selectedPerson.Model);
            }
        }

        #region Commands

        public ICommand _blankLongCommand;
        public ICommand BlankLongCommand
        {
            get { return _blankLongCommand; }
            set { SetProperty(ref _blankLongCommand, value); }
        }

        public ICommand _blankLongAsyncCommand;
        public ICommand BlankLongAsyncCommand
        {
            get { return _blankLongAsyncCommand; }
            set { SetProperty(ref _blankLongAsyncCommand, value); }
        }

        public ICommand _passEventParamsCommand;
        public ICommand PassEventParamsCommand
        {
            get { return _passEventParamsCommand; }
            set { SetProperty(ref _passEventParamsCommand, value); }
        }

        #endregion Commands

        public LeftPanelViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            BlankLongCommand = new DelegateCommand(BlankLongExecute);
            BlankLongAsyncCommand = DelegateCommand.FromAsyncHandler(BlankLongAsyncExecute);
            PassEventParamsCommand = new DelegateCommand<EventArgs>(PassEventParamsExecute);

            PeopleProvider peopleProvider = new PeopleProvider();
            People = new ObservableCollection<PersonListItemViewModel>(peopleProvider.GetPeople().Select(x => new PersonListItemViewModel(x)));
        }

        #region Command executes

        private void BlankLongExecute()
        {
            HeavyOperation();
            _eventAggregator.GetEvent<UserActionHappenedEvent>().Publish("Blank long command executed");
        }

        private async Task BlankLongAsyncExecute()
        {
            await Task.Run(new Action(HeavyOperation));
            _eventAggregator.GetEvent<UserActionHappenedEvent>().Publish("Blank long async command executed");
        }

        private void PassEventParamsExecute(EventArgs args)
        {
            _eventAggregator.GetEvent<UserActionHappenedEvent>().Publish("Pass event args params command executed");
        }

        #endregion Command executes

        private void HeavyOperation()
        {
            Thread.Sleep(5000);
        }
    }
}
