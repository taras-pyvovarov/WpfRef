using Common.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

using Microsoft.Practices.Unity;
using Module.People.ViewModels;
using Module.People.Views;
using Prism.Modularity;
using Prism.Regions;
using System.ComponentModel;

namespace Module.People.ViewModels
{
    public class MainPanelViewModel : BindableBase, IDataErrorInfo
    {
        private IUnityContainer _container;

        #region Commands

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

        public ICommand _dialogCommand;
        public ICommand DialogCommand
        {
            get { return _dialogCommand; }
            set { SetProperty(ref _dialogCommand, value); }
        }

        public ICommand _passEventParamsCommand;
        public ICommand PassEventParamsCommand
        {
            get { return _passEventParamsCommand; }
            set { SetProperty(ref _passEventParamsCommand, value); }
        }

        #endregion Commands

        public string _validatedText;
        public string ValidatedText
        {
            get { return _validatedText; }
            set { SetProperty(ref _validatedText, value); }
        }

        public MainPanelViewModel(IUnityContainer container)
        {
            _container = container;

            BlankLongCommand = new DelegateCommand(BlankLongExecute);
            BlankLongAsyncCommand = DelegateCommand.FromAsyncHandler(BlankLongAsyncExecute);
            DialogCommand = new DelegateCommand(DialogExecute);
            PassEventParamsCommand = new DelegateCommand(PassEventParamsExecute);
        }

        #region Command executes

        private void BlankLongExecute()
        {
            HeavyOperation();
        }

        private async Task BlankLongAsyncExecute()
        {
            await Task.Run(new Action(HeavyOperation));
        }

        private void DialogExecute()
        {
            IWindowService windowService = _container.Resolve<IWindowService>();
            windowService.ShowDialog(new ActionPanelViewModel(), _container.Resolve<Dictionary<Type, Type>>());
        }

        private void PassEventParamsExecute()
        {
            
        }

        #endregion Command executes

        private void HeavyOperation()
        {
            Thread.Sleep(5000);
        }

        private string Validate(string propertyName)
        {
            // Return error message if there is error on else return empty or null string

            string validationMessage = null;
            switch (propertyName)
            {
                case nameof(ValidatedText):
                    if (ValidateValidatedText())
                        return null;
                    validationMessage = "Error";
                    break;
            }

            return validationMessage;
        }

        private bool ValidateValidatedText()
        {
            if (string.IsNullOrEmpty(ValidatedText))
                return true;

            foreach (var singleChar in ValidatedText)
            {
                if (singleChar < '1' || singleChar > '9')
                    return false;
            }

            return true;
        }

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
