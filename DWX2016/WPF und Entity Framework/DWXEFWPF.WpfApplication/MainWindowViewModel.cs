using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using DWXEFWPF.DataAccess;
using DWXEFWPF.WpfApplication.Annotations;

namespace DWXEFWPF.WpfApplication
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Person> persons;
        private Person selectedPerson;
        private EditPersonViewModel editPersonViewModel;
        public ICommand LoadDataCommand { get; }

        public MainWindowViewModel()
        {
            LoadDataCommand = new RelayCommand(LoadData);
        }

        public ObservableCollection<Person> Persons
        {
            get { return persons; }
            set
            {
                if (Equals(value, persons)) return;
                persons = value;
                OnPropertyChanged();
            }
        }

        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set
            {
                if (Equals(value, selectedPerson)) return;
                selectedPerson = value;
                OnPropertyChanged();
                if (value != null)
                {
                    EditPersonViewModel = new EditPersonViewModel(value);
                }
                else
                {
                    EditPersonViewModel = null;
                }
            }
        }

        public EditPersonViewModel EditPersonViewModel
        {
            get { return editPersonViewModel; }
            set
            {
                if (Equals(value, editPersonViewModel)) return;
                editPersonViewModel = value;
                OnPropertyChanged();
            }
        }

        private async void LoadData()
        {
            var result = await Task.Run(() =>
            {
                using (var db = new WpfDemoContext())
                {
                    return db.Persons.AsNoTracking().ToList();
                }
            });

            Persons = new ObservableCollection<Person>(result);
            //Persons.Clear();
            //foreach(var item in result)
            //    Persons.Add(item);
            //Persons.ReplaceItems(result);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class EditPersonViewModel
    {
        private readonly Person value;
        public ValidatingProperty<string, Person> Vorname { get; set; }
        public string Nachname { get; set; }
        public EditPersonViewModel(Person value)
        {
            this.value = value;
            Vorname = new ValidatingProperty<string, Person>(nameof(Person.Vorname));//value.Vorname;
            Nachname = value.Nachname;
            SaveCommand = new RelayCommand(Save);
        }

        public RelayCommand SaveCommand { get; }

        private async void Save()
        {
            using (var db = new WpfDemoContext())
            {
                db.Persons.Attach(value);
                db.Entry(value).State = EntityState.Modified;
                //var person = db.Persons.Find(value.Id);
                value.Vorname = Vorname.Value;
                value.Nachname = Nachname;
                await db.SaveChangesAsync();
            }
        }
    }

    public class ValidatingProperty<TValue, TTableType> : IDataErrorInfo, INotifyPropertyChanged
    {
        private readonly string propertyName;
        private TValue value;

        public TValue Value
        {
            get { return value; }
            set
            {
                if (Equals(value, this.value)) return;
                this.value = value;
                OnPropertyChanged();
            }
        }

        public bool IsValid { get; set; }

        public ValidatingProperty(string propertyName)
        {
            this.propertyName = propertyName;
        }

        private List<Func<TValue, bool>> additonalValidationFunctions = new List<Func<TValue, bool>>();

        public void AddValidationRule(Func<TValue, bool> func)
        {
            additonalValidationFunctions.Add(func);
        } 

        public string this[string columnName]
        {
            get
            {
                var propertyInfo = typeof (TTableType).GetProperty(propertyName);
                var attributes = propertyInfo.GetCustomAttributes(typeof (ValidationAttribute), true).OfType<ValidationAttribute>().ToList();

                List<ValidationResult> result = new List<ValidationResult>();
                Validator.TryValidateValue(Value, new ValidationContext(Value), result, attributes);
                if (result.Count > 0)
                    return result[0].ErrorMessage;
                foreach (var validator in additonalValidationFunctions)
                {
                    if (validator(Value))
                    {
                        
                    }
                }
                return string.Empty;
            }
        }

        public string Error { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName1 = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName1));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action action;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action();
        }

        public RelayCommand(Action action)
        {
            this.action = action;
        }

        public event EventHandler CanExecuteChanged;
    }
}