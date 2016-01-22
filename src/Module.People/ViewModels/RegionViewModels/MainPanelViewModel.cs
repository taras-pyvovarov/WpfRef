using System;
using System.Windows.Input;
using Common.Interfaces;
using Microsoft.Practices.Unity;
using People.Domain;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Events;
using Module.People.PubSubEvents;
using Presentation.GlobalPubSubEvents;
using Prism.Regions;
using Common;

namespace Module.People.ViewModels
{
    public class MainPanelViewModel : BindableBase
    {
        private IUnityContainer _container;
        private IEventAggregator _eventAggregator;
        
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

        public DelegateCommand _navigateProductsCommand;
        public DelegateCommand NavigateProductsCommand
        {
            get { return _navigateProductsCommand; }
            set { SetProperty(ref _navigateProductsCommand, value); }
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
                if (_selectedPersonViewModel is EditPersonViewModel)
                {
                    var editVM = (EditPersonViewModel)_selectedPersonViewModel;
                    editVM.EditApplied -= editPersonViewModel_EditApplied;
                    editVM.EditCanceled -= editPersonViewModel_EditCanceled;
                }

                if (value is EditPersonViewModel)
                {
                    var editVM = (EditPersonViewModel)value;
                    editVM.EditApplied += editPersonViewModel_EditApplied;
                    editVM.EditCanceled += editPersonViewModel_EditCanceled;
                }

                SetProperty(ref _selectedPersonViewModel, value);
                EvaluatePersonActionCanExecute();
            }
        }

        public MainPanelViewModel(IUnityContainer container, IEventAggregator eventAggregator)
        {
            _container = container;
            _eventAggregator = eventAggregator;

            ShowPersonDialogCommand = new DelegateCommand(ShowPersonDialogExecute, PersonActionCanExecute);
            EditPersonCommand = new DelegateCommand(EditPersonExecute, PersonActionCanExecute);
            EditPersonDialogCommand = new DelegateCommand(EditPersonDialogExecute, PersonActionCanExecute);
            NavigateProductsCommand = new DelegateCommand(NavigateProductsExecute);

            eventAggregator.GetEvent<PersonSelectionChangedEvent>().Subscribe(PersonSelectionChanged);
        }

        #region Command executes

        private void ShowPersonDialogExecute()
        {
            IWindowService windowService = _container.Resolve<IWindowService>();
            var showPersonViewModel = new PersonViewModel(SelectedPerson);
            windowService.ShowDialog(showPersonViewModel);
        }

        private void EditPersonExecute()
        {
            IValidationService validationService = _container.Resolve<IValidationService>();
            var editPersonViewModel = new EditPersonViewModel(SelectedPerson, validationService);
            SelectedPersonViewModel = editPersonViewModel;
            _eventAggregator.GetEvent<UserActionHappenedEvent>().Publish("Person edit view switched");
        }

        private void EditPersonDialogExecute()
        {
            IWindowService windowService = _container.Resolve<IWindowService>();
            IValidationService validationService = _container.Resolve<IValidationService>();
            var editPersonViewModel = new EditPersonViewModel(SelectedPerson, validationService);
            bool? dialogResult = windowService.ShowDialog(editPersonViewModel);
            if (dialogResult == true)
                editPersonViewModel_EditApplied(editPersonViewModel, EventArgs.Empty);
            else
                editPersonViewModel_EditCanceled(editPersonViewModel, EventArgs.Empty);

            _eventAggregator.GetEvent<UserActionHappenedEvent>().Publish("Person edit dialog opened");
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

        private void NavigateProductsExecute()
        {
            IRegionManager regionManager = _container.Resolve<IRegionManager>();
            Uri ProductLeftViewUri = new Uri("ProductLeftView", UriKind.Relative);
            Uri ProductMainViewUri = new Uri("ProductMainView", UriKind.Relative);
            regionManager.RequestNavigate(AppConstants.LeftRegion, ProductLeftViewUri);
            regionManager.RequestNavigate(AppConstants.MainRegion, ProductMainViewUri);
        }

        #endregion Command executes

        private void PersonSelectionChanged(Tuple<Person, bool> payload)
        {
            if (payload.Item1 == null)
            {
                SelectedPersonViewModel = null;
                SelectedPerson = null;
                return;
            }

            SelectedPerson = payload.Item1;
            if (payload.Item2)
                SelectedPersonViewModel = new EditPersonViewModel(SelectedPerson, _container.Resolve<IValidationService>());
            else
                SelectedPersonViewModel = new PersonViewModel(SelectedPerson);
        }

        private void editPersonViewModel_EditApplied(object sender, EventArgs e)
        {
            EditPersonViewModel senderVM = (EditPersonViewModel)sender;
            SelectedPersonViewModel = new PersonViewModel(senderVM.Model);

            _eventAggregator.GetEvent<PersonChangedEvent>().Publish(SelectedPerson);
            _eventAggregator.GetEvent<UserActionHappenedEvent>().Publish("Person edit applied");
        }

        private void editPersonViewModel_EditCanceled(object sender, EventArgs e)
        {
            EditPersonViewModel senderVM = (EditPersonViewModel)sender;
            SelectedPersonViewModel = new PersonViewModel(senderVM.Model);
            _eventAggregator.GetEvent<UserActionHappenedEvent>().Publish("Person edit canceled");
        }
    }
}
