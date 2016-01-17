namespace Common.Interfaces
{
    public interface IValidationService
    {
        string[] ValidateName(string nameToValidate);
        string[] ValidatePhoneNumber(string phoneNumberToValidate);
    }
}
