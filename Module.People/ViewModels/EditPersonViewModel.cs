using System.ComponentModel;
using People.Domain;

namespace Module.People.ViewModels
{
    public class EditPersonViewModel : PersonViewModel, IDataErrorInfo
    {
        public EditPersonViewModel(Person personModel) : base(personModel)
        {
        }

        #region Validation

        private string Validate(string propertyName)
        {
            // Return error message if there is error on else return empty or null string
            string validationMessage = null;
            switch (propertyName)
            {
                case nameof(Firstname):
                    if (ValidateFirstname())
                        return null;
                    validationMessage = "Firstname is incorrect";
                    break;
                case nameof(Lastname):
                    if (ValidateLastname())
                        return null;
                    validationMessage = "Lastname is incorrect";
                    break;
                case nameof(ValidatePhoneNumber):
                    if (ValidateFirstname())
                        return null;
                    validationMessage = "Phone number is incorrect";
                    break;
            }

            return validationMessage;
        }

        private bool ValidateFirstname()
        {
            if (string.IsNullOrEmpty(Firstname))
                return true;

            foreach (var singleChar in Firstname)
            {
                if (singleChar < '1' || singleChar > '9')
                    return false;
            }

            return true;
        }

        private bool ValidateLastname()
        {
            if (string.IsNullOrEmpty(Firstname))
                return true;

            foreach (var singleChar in Firstname)
            {
                if (singleChar < '1' || singleChar > '9')
                    return false;
            }

            return true;
        }

        private bool ValidatePhoneNumber()
        {
            if (string.IsNullOrEmpty(Firstname))
                return true;

            foreach (var singleChar in Firstname)
            {
                if (singleChar < '1' || singleChar > '9')
                    return false;
            }

            return true;
        }

        #endregion Validation

        #region IDataErrorInfo implementation

        public string Error
        {
            get { return "Error"; }
        }

        public string this[string columnName]
        {
            get { return Validate(columnName); }
        }

        #endregion IDataErrorInfo implementation
    }
}
