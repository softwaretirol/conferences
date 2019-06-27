using System;

namespace DwxWorkshop.AppFx
{
    public class RefProperty<T> : BindingBase
    {
        private readonly Func<T> readFunc;
        private readonly Action<T> writeAction;

        public T Data
        {
            get => readFunc();
            set
            {
                writeAction(value);
                OnPropertyChanged(nameof(Data));
            }
        }

        public RefProperty(Func<T> readFunc, Action<T> writeAction)
        {
            this.readFunc = readFunc;
            this.writeAction = writeAction;
        }
    }
}