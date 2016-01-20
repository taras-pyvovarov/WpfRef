using System;
using System.Windows.Input;
using Common.Interfaces;
using Microsoft.Practices.Unity;
using People.Domain;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Events;
using Module.People.PubSubEvents;

namespace Module.People.ViewModels
{
    public class MainPanelViewModel : BindableBase
    {
        private IUnityContainer _container;
        
        #region Commands

        public DelegateCommand _showPersonDialogCommand;
        public DelegateCommand ShowPersonDialogCommand
        {
            get { return _showPersonDialogCommand; }
            set { SetProperty(ref _showPersonDialogCommand, value); }
        }

        public DelegateCommand _editPersonCommand;
        public DelegateCommand EditPersonCommand
        {
            get { return _editPersonCommand; }
            set { SetProperty(ref _editPersonCommand, value); }
        }

        public DelegateCommand _editPersonDialogCommand;
        public DelegateCommand EditPersonDialogCommand
        {
            get { return _editPersonDialogCommand; }
            set { SetProperty(ref _editPersonDialogCommand, value); }
        }

        #endregion Commands

        public Person _selectedPerson;
        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            private set { SetProperty(ref _selectedPerson, value); }
        }

        public object _selectedPersonViewModel;
        public object SelectedPersonViewModel
        {
            get { return _selectedPersonViewModel; }
            private set 
            { 
                SetProperty(ref _selectedPersonViewModel, value);
                EvaluatePersonActionCanExecute();
            }
        }

        public MainPanelViewModel(IUnityContainer container, IEventAggregator eventAggregator)
        {
            _container = container;

            ShowPersonDialogCommand = new DelegateCommand(ShowPersonDialogExecute, PersonActionCanExecute);
            EditPersonCommand = new DelegateCommand(EditPersonExecute, PersonActionCanExecute);
            EditPersonDialogCommand = new DelegateCommand(EditPersonDialogExecute, PersonActionCanExecute);

            eventAggregator.GetEvent<SelectedPersonChangedEvent>().Subscribe(SelectedPersonChanged);
        }

        #region Command executes

        private void ShowPersonDialogExecute()
        {
            IWindowService windowService = _container.Resolve<IWindowService>();
            var showPersonViewModel = new ShowPersonViewModel(new Person("Name1", "Lastname1", "7658675"));
            windowService.ShowDialog(showPersonViewModel);
        }

        private void EditPersonExecute()
        {
            IValidationService validationService = _container.Resolve<IValidationService>();
            var editPersonViewModel = new EditPersonViewModel(new Person("Name1", "Lastname1", "7658675"), validationService);
            editPersonViewModel.EditApplied += editPersonViewModel_EditApplied;
            editPersonViewModel.EditCanceled += editPersonViewModel_EditCanceled;
            SelectedPersonViewModel = editPersonViewModel;
        }

        private void EditPersonDialogExecute()
        {
            IWindowService windowService = _container.Resolve<IWindowService>();
            IValidationService validationService = _container.Resolve<IValidationService>();
            var editPersonViewModel = new EditPersonViewModel(new Person("Name1", "Lastname1", "7658675"), validationService);
            bool? a = windowService.ShowDialog(editPersonViewModel);
        }

        private bool PersonActionCanExecute()
        {
            return SelectedPersonViewModel != null;
        }

        private void EvaluatePersonActionCanExecute()
        {
            ShowPersonDialogCommand.RaiseCanExecuteChanged();
            EditPersonCommand.RaiseCanExecuteChanged();
            EditPersonDialogCommand.RaiseCanExecuteChanged();
        }

        #endregion Command executes

        private void SelectedPersonChanged(Person person)
        {
            if (person == null)
            {
                SelectedPersonViewModel = null;
                return;
            }

            SelectedPersonViewModel = new ShowPersonViewModel(person);
        }

        private void editPersonViewModel_EditApplied(object sender, EventArgs e)
        {
            EditPersonViewModel senderVM = (EditPersonViewModel)sender;
            senderVM.EditApplied -= editPersonViewModel_EditApplied;
            senderVM.EditCanceled -= editPersonViewModel_EditCanceled;

            SelectedPersonViewModel = new ShowPersonViewModel(new Person("Name1", "Lastname1", "7658675"));
        }

        private void editPersonViewModel_EditCanceled(object sender, EventArgs e)
        {
            EditPersonViewModel senderVM = (EditPersonViewModel)sender;
            senderVM.EditApplied -= editPersonViewModel_EditApplied;
            senderVM.EditCanceled -= editPersonViewModel_EditCanceled;

            SelectedPersonViewModel = new ShowPersonViewModel(new Person("Name1", "Lastname1", "7658675"));
        }
    }
}
