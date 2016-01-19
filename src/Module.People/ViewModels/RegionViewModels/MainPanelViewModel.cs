using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Common.Interfaces;
using Microsoft.Practices.Unity;
using People.Domain;
using Prism.Commands;
using Prism.Mvvm;

namespace Module.People.ViewModels
{
    public class MainPanelViewModel : BindableBase
    {
        private IUnityContainer _container;

        #region Commands

        public ICommand _showPersonDialogCommand;
        public ICommand ShowPersonDialogCommand
        {
            get { return _showPersonDialogCommand; }
            set { SetProperty(ref _showPersonDialogCommand, value); }
        }

        public ICommand _editPersonCommand;
        public ICommand EditPersonCommand
        {
            get { return _editPersonCommand; }
            set { SetProperty(ref _editPersonCommand, value); }
        }

        public ICommand _editPersonDialogCommand;
        public ICommand EditPersonDialogCommand
        {
            get { return _editPersonDialogCommand; }
            set { SetProperty(ref _editPersonDialogCommand, value); }
        }

        public ICommand _blankLongCommand;
        public ICommand BlankLongCommand
        {
            get { return _blankLongCommand; }
            set { SetProperty(ref _blankLongCommand, value); }
        }

        public ICommand _blankLongAsyncCommand;
        public ICommand BlankLongAsyncCommand
        {
            get { return _blankLongAsyncCommand; }
            set { SetProperty(ref _blankLongAsyncCommand, value); }
        }
                
        public ICommand _passEventParamsCommand;
        public ICommand PassEventParamsCommand
        {
            get { return _passEventParamsCommand; }
            set { SetProperty(ref _passEventParamsCommand, value); }
        }

        #endregion Commands

        public object _selectedPersonViewModel;
        public object SelectedPersonViewModel
        {
            get { return _selectedPersonViewModel; }
            private set { SetProperty(ref _selectedPersonViewModel, value); }
        }

        public MainPanelViewModel(IUnityContainer container)
        {
            _container = container;

            SelectedPersonViewModel = new ShowPersonViewModel(new Person("Name1", "Lastname1", "7658675"));

            ShowPersonDialogCommand = new DelegateCommand(ShowPersonDialogExecute);
            EditPersonCommand = new DelegateCommand(EditPersonExecute);
            EditPersonDialogCommand = new DelegateCommand(EditPersonDialogExecute);
            BlankLongCommand = new DelegateCommand(BlankLongExecute);
            BlankLongAsyncCommand = DelegateCommand.FromAsyncHandler(BlankLongAsyncExecute);
            PassEventParamsCommand = new DelegateCommand<EventArgs>(PassEventParamsExecute);
        }

        #region Command executes

        private void ShowPersonDialogExecute()
        {
            IWindowService windowService = _container.Resolve<IWindowService>();
            var showPersonViewModel = new ShowPersonViewModel(new Person("Name1", "Lastname1", "7658675"));
            windowService.ShowDialog(showPersonViewModel);
        }

        private void EditPersonExecute()
        {
            IValidationService validationService = _container.Resolve<IValidationService>();
            var editPersonViewModel = new EditPersonViewModel(new Person("Name1", "Lastname1", "7658675"), validationService);
            editPersonViewModel.EditApplied += editPersonViewModel_EditApplied;
            editPersonViewModel.EditCanceled += editPersonViewModel_EditCanceled;
            SelectedPersonViewModel = editPersonViewModel;
        }

        private void EditPersonDialogExecute()
        {
            IWindowService windowService = _container.Resolve<IWindowService>();
            IValidationService validationService = _container.Resolve<IValidationService>();
            var editPersonViewModel = new EditPersonViewModel(new Person("Name1", "Lastname1", "7658675"), validationService);
            bool? a = windowService.ShowDialog(editPersonViewModel);
        }

        private void BlankLongExecute()
        {
            HeavyOperation();
        }

        private async Task BlankLongAsyncExecute()
        {
            await Task.Run(new Action(HeavyOperation));
        }

        private void PassEventParamsExecute(EventArgs args)
        {
            
        }

        #endregion Command executes

        private void editPersonViewModel_EditApplied(object sender, EventArgs e)
        {
            EditPersonViewModel senderVM = (EditPersonViewModel)sender;
            senderVM.EditApplied -= editPersonViewModel_EditApplied;
            senderVM.EditCanceled -= editPersonViewModel_EditCanceled;

            SelectedPersonViewModel = new ShowPersonViewModel(new Person("Name1", "Lastname1", "7658675"));
        }

        private void editPersonViewModel_EditCanceled(object sender, EventArgs e)
        {
            EditPersonViewModel senderVM = (EditPersonViewModel)sender;
            senderVM.EditApplied -= editPersonViewModel_EditApplied;
            senderVM.EditCanceled -= editPersonViewModel_EditCanceled;

            SelectedPersonViewModel = new ShowPersonViewModel(new Person("Name1", "Lastname1", "7658675"));
        }

        private void HeavyOperation()
        {
            Thread.Sleep(5000);
        }
    }
}
