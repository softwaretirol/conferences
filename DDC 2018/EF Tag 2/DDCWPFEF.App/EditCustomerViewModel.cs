using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using DDCWPFEF.Data.Queries.Contracts;

namespace DDCWPFEF.App
{
    public class Property
    {
        public static Property<T> Create<T>(T value)
        {
            return new Property<T>(value);
        }
        public static Property<T> Create<T>()
        {
            return new Property<T>();
        }
    }
    public class Property<T> : BindableBase, IDataErrorInfo
    {
        private readonly T originalValue;

        public Property()
        {

        }

        public bool HasChanged => !Equals(originalValue, Value);
        public Property(T originalValue)
        {
            Value = this.originalValue = originalValue;
        }
        private T value;

        public T Value
        {
            get => value;
            set
            {
                SetProperty(ref this.value, value);
                OnPropertyChanged(nameof(HasChanged));
            }
        }

        private Func<T, string> validationFunc;

        public Property<T> ValidateWith(Func<T, string> validationFunc)
        {
            this.validationFunc = validationFunc;
            return this;
        }
        public string this[string propertyName]
        {
            get { return validationFunc?.Invoke(Value); }
        }

        public string Error { get; }
    }
    public class EditCustomerViewModel : BindableBase//, IDataErrorInfo
    {
        private readonly ICustomerQueries customerQueries;
        public int Id { get; }
        public Property<string> FirstName { get; }
        public Property<string> CompanyName { get; }
        public Property<string> Street { get; }
        public Property<int?> PostalCode { get; }
        public Property<string> City { get; }
        public int OrderCount { get; }
        public DateTime? LastOrder { get; }
        public ICommand SaveCommand { get; }

        public EditCustomerViewModel(EditCustomerEntry customer, ICustomerQueries customerQueries)
        {
            this.customerQueries = customerQueries;
            Id = customer.Id;
            FirstName = Property.Create(customer.FirstName);
            CompanyName = Property.Create(customer.CompanyName).ValidateWith(x => x?.Length < 10 ? "Zu kurz" : null);
            Street = Property.Create(customer.Street);
            PostalCode = Property.Create(customer.PostalCode);
            City = Property.Create(customer.City);
            OrderCount = customer.OrderCount;
            LastOrder = customer.LastOrder;
            SaveCommand = new DelegateCommand(Save);
        }

        private async Task Save()
        {
            await customerQueries.SaveCustomer(new EditCustomer()
            {
                CompanyName = CompanyName.Value,
                Id = Id,
                City = City.Value,
                Street = Street.Value,
                PostalCode = PostalCode.Value,
                FirstName = FirstName.Value
            });
        }

        //public string this[string propertyName]
        //{
        //    get
        //    {
        //        switch (propertyName)
        //        {
        //            case nameof(City):
        //                break;
        //            case nameof(CompanyName):
        //                if (CompanyName?.Length < 10)
        //                {
        //                    return "CompanyName ist zu kurz";
        //                }
        //                break;
        //            case nameof(FirstName):
        //                break;
        //        }

        //        return null;
        //    }
        //}

        //public string Error { get; } // Wird in WPF ignoriert!!!
    }
}