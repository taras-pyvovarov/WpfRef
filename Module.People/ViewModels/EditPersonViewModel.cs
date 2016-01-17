using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Common.Interfaces;
using People.Domain;
using Prism.Mvvm;

namespace Module.People.ViewModels
{
    public class EditPersonViewModel : BindableBase, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, string[]> _validationErrors = new Dictionary<string, string[]>();
        private readonly IValidationService _validationService;

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
            _validationService = validationService;

            Firstname = personModel.Firstname;
            Lastname = personModel.Lastname;
            PhoneNumber = personModel.PhoneNumber;
        }

        private async void ValidateValueAsync(string validatedPropertyName, Func<string[]> validationMethod)
        {
            //bool isValid = await Task<bool>.Run(() => { return _validationService.ValidatePhoneNumber(nameToValidate, out validationErrors); }).ConfigureAwait(false);
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
