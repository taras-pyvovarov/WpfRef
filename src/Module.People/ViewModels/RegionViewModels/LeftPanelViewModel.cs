using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Presentation.PubSubEvents;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Module.People.ViewModels
{
    public class LeftPanelViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;

        private List<string> _people;
        public List<string> People
        {
            get { return _people; }
            private set { SetProperty(ref this._people, value); }
        }

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

        public ICommand _passEventParamsCommand;
        public ICommand PassEventParamsCommand
        {
            get { return _passEventParamsCommand; }
            set { SetProperty(ref _passEventParamsCommand, value); }
        }

        #endregion Commands

        public LeftPanelViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            BlankLongCommand = new DelegateCommand(BlankLongExecute);
            BlankLongAsyncCommand = DelegateCommand.FromAsyncHandler(BlankLongAsyncExecute);
            PassEventParamsCommand = new DelegateCommand<EventArgs>(PassEventParamsExecute);

            var people = new List<string>();
            people.Add("People1");
            people.Add("People2");
            people.Add("People3");
            people.Add("People4");
            people.Add("People5");

            People = people;
        }

        #region Command executes

        private void BlankLongExecute()
        {
            HeavyOperation();
            _eventAggregator.GetEvent<UserActionHappenedEvent>().Publish("Blank long command executed");
        }

        private async Task BlankLongAsyncExecute()
        {
            await Task.Run(new Action(HeavyOperation));
            _eventAggregator.GetEvent<UserActionHappenedEvent>().Publish("Blank long async command executed");
        }

        private void PassEventParamsExecute(EventArgs args)
        {
            _eventAggregator.GetEvent<UserActionHappenedEvent>().Publish("Pass event args params command executed");
        }

        #endregion Command executes

        private void HeavyOperation()
        {
            Thread.Sleep(5000);
        }
    }
}
