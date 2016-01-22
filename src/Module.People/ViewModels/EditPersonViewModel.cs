using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Common.Interfaces;
using People.Domain;
using Prism.Mvvm;
using System.Windows.Input;
using Prism.Commands;
using System.Windows.Threading;
using System.Windows;

namespace Module.People.ViewModels
{
    public class EditPersonViewModel : PersonViewModel
    {
        #region Commands

        public DelegateCommand _applyEditCommand;
        public DelegateCommand ApplyEditCommand
        {
            get { return _applyEditCommand; }
            set { SetProperty(ref _applyEditCommand, value); }
        }

        public DelegateCommand _cancelEditCommand;
        public DelegateCommand CancelEditCommand
        {
            get { return _cancelEditCommand; }
            set { SetProperty(ref _cancelEditCommand, value); }
        }

        #endregion Commands

        private bool? _dialogResult;
        public bool? DialogResult
        {
            get { return _dialogResult; }
            set { SetProperty(ref this._dialogResult, value); }
        }

        public event EventHandler EditApplied;
        public event EventHandler EditCanceled;

        public EditPersonViewModel(Person personModel, IValidationService validationService)
            : base(personModel, validationService)
        {
            ApplyEditCommand = new DelegateCommand(ApplyEditExecute, ApplyEditCanExecute);
            CancelEditCommand = new DelegateCommand(CancelEditExecute);
        }

        #region Command executes

        private bool ApplyEditCanExecute()
        {
            return !HasErrors;
        }

        private void ApplyEditExecute()
        {
            ConvertViewModelToModel(this, Model);

            DialogResult = true;
            RaiseEvent(EditApplied);
        }

        private void CancelEditExecute()
        {
            DialogResult = false;
            RaiseEvent(EditCanceled);
        }

        #endregion Command executes

        protected override void ValidationErrorsChanged()
        {
            base.ValidationErrorsChanged();

            if (ApplyEditCommand != null)
                ApplyEditCommand.RaiseCanExecuteChanged();
        }

        private void RaiseEvent(EventHandler eventToRaise)
        {
            var eventRef = eventToRaise;
            if (eventRef != null)
                eventRef(this, EventArgs.Empty);
        }
    }
}
