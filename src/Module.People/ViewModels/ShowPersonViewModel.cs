using People.Domain;
using Prism.Mvvm;

namespace Module.People.ViewModels
{
    public class ShowPersonViewModel : BindableBase
    {
        private string _firstname;
        public string Firstname
        {
            get { return _firstname; }
            set { SetProperty(ref this._firstname, value); }
        }

        private string _lastname;
        public string Lastname
        {
            get { return _lastname; }
            set { SetProperty(ref this._lastname, value); }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { SetProperty(ref this._phoneNumber, value); }
        }

        public ShowPersonViewModel(Person personModel)
        {
            Firstname = personModel.Firstname;
            Lastname = personModel.Lastname;
            PhoneNumber = personModel.PhoneNumber;
        }
    }
}
