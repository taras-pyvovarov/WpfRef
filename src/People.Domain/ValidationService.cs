using System.Collections.Generic;
using System.Linq;
using Common.Interfaces;

namespace People.Domain
{
    public class ValidationService : IValidationService
    {
        public string[] ValidateName(string nameToValidate)
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(nameToValidate))
                errors.Add("Name cannot be empty");

            foreach (var singleChar in nameToValidate)
            {
                if (!char.IsLetter(singleChar) && singleChar != ' ' && singleChar != '-')
                {
                    errors.Add("Name contains illegal chars");
                    break;
                }
            }

            char firstChar = nameToValidate.FirstOrDefault();
            if (firstChar != default(char) && !char.IsUpper(firstChar))
                errors.Add("Name should start with uppercase letter");
            return errors.ToArray();
        }

        public string[] ValidatePhoneNumber(string phoneNumberToValidate)
        {
            List<string> errors = new List<string>();

            foreach (var singleChar in phoneNumberToValidate)
            {
                if (!char.IsNumber(singleChar) && singleChar != ' ' && singleChar != '-')
                {
                    errors.Add("Phone number contains illegal chars");
                    break;
                }
            }

            return errors.ToArray();
        }
    }
}
