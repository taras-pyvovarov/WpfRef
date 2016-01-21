using People.Domain;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.People.ViewModels
{
    public class PersonViewModel : BindableBase
    {
        public Person Model { get; private set; }

        public string _firstname;
        public string Firstname 
        { 
            get { return _firstname; }
            set { SetProperty(ref _firstname, value); }
        }

        public string _lastname;
        public string Lastname
        {
            get { return _lastname; }
            set { SetProperty(ref _lastname, value); }
        }

        public string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { SetProperty(ref _phoneNumber, value); }
        }

        public PersonViewModel(Person personModel)
        {
            Model = personModel;
            ConvertModelToViewModel(Model, this);
        }

        public static void ConvertModelToViewModel(Person personModel, PersonViewModel personViewModel)
        {
            personViewModel.Firstname = personModel.Firstname;
            personViewModel.Lastname = personModel.Lastname;
            personViewModel.PhoneNumber = personModel.PhoneNumber;
        }

        public static void ConvertViewModelToModel(PersonViewModel personViewModel, Person personModel)
        {
            personModel.Firstname = personViewModel.Firstname;
            personModel.Lastname = personViewModel.Lastname;
            personModel.PhoneNumber = personViewModel.PhoneNumber;
        }
    }
}
