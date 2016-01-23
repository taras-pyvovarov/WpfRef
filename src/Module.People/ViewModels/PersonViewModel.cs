using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using Common.Interfaces;
using People.Domain;
using Prism.Mvvm;

namespace Module.People.ViewModels
{
    public class PersonViewModel : BindableBase, INotifyDataErrorInfo
    {
        protected readonly Dictionary<string, string[]> ValidationErrors = new Dictionary<string, string[]>();
        private readonly IValidationService _validationService;

        public Person Model { get; private set; }

        public string _firstname;
        public string Firstname 
        { 
            get { return _firstname; }
            set 
            { 
                SetProperty(ref _firstname, value);
                if (_validationService != null)
                    ValidateValueAsync(nameof(Firstname), () => _validationService.ValidateName(value));
            }
        }

        public string _lastname;
        public string Lastname
        {
            get { return _lastname; }
            set 
            { 
                SetProperty(ref _lastname, value);
                if (_validationService != null)
                    ValidateValueAsync(nameof(Lastname), () => _validationService.ValidateName(value));
            }
        }

        public string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set 
            { 
                SetProperty(ref _phoneNumber, value);
                if (_validationService != null)
                    ValidateValueAsync(nameof(PhoneNumber), () => _validationService.ValidatePhoneNumber(value));
            }
        }

        public PersonViewModel(Person model, IValidationService validationService = null)
        {
            Model = model;
            _validationService = validationService;
            ConvertModelToViewModel(Model, this);
        }

        private async void ValidateValueAsync(string validatedPropertyName, Func<string[]> validationMethod)
        {
            string[] validationResult = await Task<string[]>.Run(validationMethod).ConfigureAwait(false);

            if (validationResult.Length > 0)
                ValidationErrors[validatedPropertyName] = validationResult;
            else if (ValidationErrors.ContainsKey(validatedPropertyName))
                ValidationErrors.Remove(validatedPropertyName);
            RaiseErrorsChanged(validatedPropertyName);
            Application.Current.Dispatcher.Invoke(ValidationErrorsChanged);
        }

        protected virtual void ValidationErrorsChanged()
        {
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
            string[] value;
            ValidationErrors.TryGetValue(propertyName, out value);
            return value;
        }

        public bool HasErrors
        {
            get { return ValidationErrors.Count > 0; }
        }

        #endregion INotifyDataErrorInfo implementation

        public static void ConvertModelToViewModel(Person model, PersonViewModel viewModel)
        {
            viewModel.Firstname = model.Firstname;
            viewModel.Lastname = model.Lastname;
            viewModel.PhoneNumber = model.PhoneNumber;
        }

        public static void ConvertViewModelToModel(PersonViewModel viewModel, Person model)
        {
            model.Firstname = viewModel.Firstname;
            model.Lastname = viewModel.Lastname;
            model.PhoneNumber = viewModel.PhoneNumber;
        }
    }
}
