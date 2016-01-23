using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Module.People.PubSubEvents;
using People.Domain;
using Presentation.GlobalPubSubEvents;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

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
                DeletePersonCommand.RaiseCanExecuteChanged();
                _eventAggregator.GetEvent<PersonSelectionChangedEvent>().Publish(new Tuple<Person, bool>(_selectedPerson?.Model, false));

            }
        }

        #region Commands

        public DelegateCommand _addPersonCommand;
        public DelegateCommand AddPersonCommand
        {
            get { return _addPersonCommand; }
            set { SetProperty(ref _addPersonCommand, value); }
        }

        public DelegateCommand _deletePersonCommand;
        public DelegateCommand DeletePersonCommand
        {
            get { return _deletePersonCommand; }
            set { SetProperty(ref _deletePersonCommand, value); }
        }

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

            _eventAggregator.GetEvent<PersonChangedEvent>().Subscribe(PersonChanged);

            AddPersonCommand = new DelegateCommand(AddPersonExecute);
            DeletePersonCommand = new DelegateCommand(DeletePersonExecute, DeletePersonCanExecute);
            BlankLongCommand = new DelegateCommand(BlankLongExecute);
            BlankLongAsyncCommand = DelegateCommand.FromAsyncHandler(BlankLongAsyncExecute);
            PassEventParamsCommand = new DelegateCommand<EventArgs>(PassEventParamsExecute);

            PeopleProvider peopleProvider = new PeopleProvider();
            People = new ObservableCollection<PersonListItemViewModel>(peopleProvider.GetPeople().Select(x => new PersonListItemViewModel(x)));
        }

        #region Command executes

        private void AddPersonExecute()
        {
            var newPerson = new Person(string.Empty, string.Empty, string.Empty);
            _eventAggregator.GetEvent<PersonSelectionChangedEvent>().Publish(new Tuple<Person, bool>(newPerson, true));
        }

        private bool DeletePersonCanExecute()
        {
            return SelectedPerson != null;
        }

        private void DeletePersonExecute()
        {
            People.Remove(SelectedPerson);
            _eventAggregator.GetEvent<UserActionHappenedEvent>().Publish("Person was removed");
        }

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

        private void PersonChanged(Person person)
        {
            PersonListItemViewModel existingPersonViewModel = People.SingleOrDefault(x => x.Model == person);
            if (existingPersonViewModel != null)
                PersonListItemViewModel.ConvertModelToViewModel(person, existingPersonViewModel);
            else
            {
                People.Add(new PersonListItemViewModel(person));
                _eventAggregator.GetEvent<UserActionHappenedEvent>().Publish("New person added");
            }
                
        }

        private void HeavyOperation()
        {
            Thread.Sleep(5000);
        }
    }
}
