using System.Collections.Generic;
using Common.Interfaces;

namespace People.Domain
{
    public class ValidationService : IValidationService
    {
        public string[] ValidateName(string nameToValidate)
        {
            List<string> errors = new List<string>();
            errors.Add("test error");
            errors.Add("test error2");
            return errors.ToArray();
        }

        public string[] ValidatePhoneNumber(string phoneNumberToValidate)
        {
            List<string> errors = new List<string>();
            errors.Add("test error3");
            return errors.ToArray();
        }
    }
}
