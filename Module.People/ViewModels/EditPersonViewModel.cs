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

namespace Module.People.ViewModels
{
    public class EditPersonViewModel : BindableBase, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, string[]> _validationErrors = new Dictionary<string, string[]>();
        private readonly IValidationService _validationService;
        private readonly Person editingPerson;

        #region Commands

        public ICommand _applyEditCommand;
        public ICommand ApplyEditCommand
        {
            get { return _applyEditCommand; }
            set { SetProperty(ref _applyEditCommand, value); }
        }

        #endregion Commands

        private string _firstname;
        public string Firstname
        {
            get { return _firstname; }
            set
            {
                SetProperty(ref this._firstname, value);
                ValidateValueAsync(nameof(Firstname), () => _validationService.ValidateName(value));
            }
        }

        private string _lastname;
        public string Lastname
        {
            get { return _lastname; }
            set
            {
                SetProperty(ref this._lastname, value);
                ValidateValueAsync(nameof(Lastname), () => _validationService.ValidateName(value));
            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                SetProperty(ref this._phoneNumber, value);
                ValidateValueAsync(nameof(PhoneNumber), () => _validationService.ValidatePhoneNumber(value));
            }
        }

        public EditPersonViewModel(Person personModel, IValidationService validationService)
        {
            ApplyEditCommand = new DelegateCommand(ApplyEditExecute);

            _validationService = validationService;

            editingPerson = personModel;
            Firstname = personModel.Firstname;
            Lastname = personModel.Lastname;
            PhoneNumber = personModel.PhoneNumber;
        }

        #region Command executes

        private void ApplyEditExecute()
        {
            editingPerson.Firstname = Firstname;
            editingPerson.Lastname = Lastname;
            editingPerson.PhoneNumber = PhoneNumber;
        }

        #endregion Command executes

        private async void ValidateValueAsync(string validatedPropertyName, Func<string[]> validationMethod)
        {
            string[] validationResult = await Task<string[]>.Run(validationMethod).ConfigureAwait(false);

            if (validationResult.Length > 0)
                _validationErrors[validatedPropertyName] = validationResult;
            else if (_validationErrors.ContainsKey(validatedPropertyName))
                _validationErrors.Remove(validatedPropertyName);
            RaiseErrorsChanged(validatedPropertyName);
        }

        #region INotifyDataErrorInfo implementation

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private void RaiseErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !_validationErrors.ContainsKey(propertyName))
                return null;

            return _validationErrors[propertyName];
        }

        public bool HasErrors
        {
            get { return _validationErrors.Count > 0; }
        }

        #endregion INotifyDataErrorInfo implementation
    }
}
