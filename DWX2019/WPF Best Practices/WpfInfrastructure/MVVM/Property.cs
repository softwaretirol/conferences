using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace DwxWorkshop.AppFx
{
    public class Property<T> : BindingBase, INotifyDataErrorInfo
    {
        private List<Func<T, string>> validationRules = new List<Func<T, string>>();
        private T data;
        private T initial;

        public Property()
        {

        }

        public Property(T intialData)
        {
            Data = this.initial = intialData;
        }
        public T Data
        {
            get => data;
            set
            {
                SetProperty(ref data, value);
                OnPropertyChanged(nameof(HasChanges));
            }
        }

        public bool HasChanges => !Equals(initial, Data);

        public Property<T> AddValidationRule(Func<T, string> validationRule)
        {
            validationRules.Add(validationRule);
            return this;
        }

        public IEnumerable GetErrors(string propertyName)
        {
            foreach (var rule in validationRules)
            {
                var validationResult = rule(Data);
                if (!string.IsNullOrEmpty(validationResult))
                {
                    yield return validationResult;
                }
            }
        }

        public bool HasErrors
        {
            get
            {
                foreach (var rule in validationRules)
                {
                    var validationResult = rule(Data);
                    if (!string.IsNullOrEmpty(validationResult))
                        return true;
                }

                return false;
            }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
    }
}