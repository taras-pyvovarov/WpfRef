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

namespace Module.People.ViewModels
{
    public class MainPanelViewModel : BindableBase
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

        #endregion Commands

        public MainPanelViewModel(IUnityContainer container)
        {
            _container = container;

            BlankLongCommand = new DelegateCommand(BlankLongExecute);
            BlankLongAsyncCommand = DelegateCommand.FromAsyncHandler(BlankLongAsyncExecute);
            DialogCommand = new DelegateCommand(DialogExecute);
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

        #endregion Command executes

        private void HeavyOperation()
        {
            Thread.Sleep(5000);
        }
    }
}
