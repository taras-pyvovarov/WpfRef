using People.Domain;
using Prism.Mvvm;

namespace Module.People.ViewModels
{
    public class PersonListItemViewModel : BindableBase
    {
        public Person Model { get; private set; }

        public string _lastname;
        public string Lastname
        {
            get { return _lastname; }
            set { SetProperty(ref _lastname, value); }
        }

        public PersonListItemViewModel(Person personModel)
        {
            Model = personModel;
            Lastname = Model.Lastname;
        }

        public static void ConvertModelToViewModel(Person model, PersonListItemViewModel viewModel)
        {
            viewModel.Lastname = model.Lastname;
        }
    }
}
