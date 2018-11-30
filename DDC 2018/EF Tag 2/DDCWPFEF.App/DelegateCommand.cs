using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DDCWPFEF.App
{
    public class DelegateCommand : BindableBase, ICommand
    {
        private readonly Func<Task> toExecute;
        private bool isExecuting;

        public bool IsExecuting
        {
            get => isExecuting;
            private set
            {
                SetProperty(ref isExecuting, value);
                OnCanExecuteChanged();
            }
        }

        public DelegateCommand(Func<Task> toExecute)
        {
            this.toExecute = toExecute;
        }
        public bool CanExecute(object parameter)
        {
            return !IsExecuting;
        }

        public async void Execute(object parameter)
        {
            if (IsExecuting)
            {
                return;
            }

            IsExecuting = true;
            await toExecute();
            IsExecuting = false;
        }

        public event EventHandler CanExecuteChanged;

        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}