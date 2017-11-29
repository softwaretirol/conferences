using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NorthwindApp.WpfApp.MVVM
{
    public class DelegateCommand : ICommand
    {
        private readonly Func<Task> action;
        private bool isExecuting;

        public DelegateCommand(Func<Task> action)
        {
            this.action = action;
        }
        public bool CanExecute(object parameter)
        {
            return !IsExecuting;
        }

        public bool IsExecuting
        {
            get { return isExecuting; }
            private set
            {
                isExecuting = value;
                OnCanExecuteChanged();
            }
        }

        public async void Execute(object parameter)
        {
            IsExecuting = true;
            await action();
            IsExecuting = false;
        }

        public event EventHandler CanExecuteChanged;

        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}