﻿using System;
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

        private void PersonSelectionChanged(Person person)
        {
            if (person == null)
            {
                SelectedPersonViewModel = null;
                SelectedPerson = null;
                return;
            }

            SelectedPerson = person;
            SelectedPersonViewModel = new PersonViewModel(SelectedPerson);
        }

        private void editPersonViewModel_EditApplied(object sender, EventArgs e)
        {
            EditPersonViewModel senderVM = (EditPersonViewModel)sender;
            SelectedPersonViewModel = new PersonViewModel(senderVM.Model);

            _eventAggregator.GetEvent<PersonChangedEvent>().Publish(SelectedPerson);
        }

        private void editPersonViewModel_EditCanceled(object sender, EventArgs e)
        {
            EditPersonViewModel senderVM = (EditPersonViewModel)sender;
            SelectedPersonViewModel = new PersonViewModel(senderVM.Model);
        }
    }
}
