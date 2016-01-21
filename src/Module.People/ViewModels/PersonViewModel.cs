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

        public PersonViewModel(Person model)
        {
            Model = model;
            ConvertModelToViewModel(Model, this);
        }

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
