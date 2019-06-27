using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DwxWorkshop.AppFx
{
    public class PersonViewModel : BindingBase, INotifyDataErrorInfo
    {
        private readonly Person person;
        private int counter;

        public AsyncProperty<ObservableCollection<Person>> AllPersons { get; }

        public string Vorname
        {
            get => person.Vorname;
            set
            {
                person.Vorname = value;
                OnPropertyChanged(nameof(Vorname));
            }
        }

        public int Counter
        {
            get => counter;
            set => SetProperty(ref counter, value);
        }

        public PersonViewModel(Person person)
        {
            this.person = person;
            AllPersons = new AsyncProperty<ObservableCollection<Person>>(LoadAllPersons);
        }

        private async Task<ObservableCollection<Person>> LoadAllPersons()
        {
            return await Task.Run(() =>
            {
                Thread.Sleep(3000);
                return new ObservableCollection<Person>(Enumerable.Range(0, 1000).Select(x => new Person
                {
                    Vorname = x.ToString()
                }));
            });
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (propertyName == nameof(Vorname))
            {
                yield return "Vorname ist ungültig";
            }
        }

        public bool HasErrors => !IsValid();

        private bool IsValid()
        {
            if (Vorname?.Length > 100)
                return false;
            if (Vorname?.Length < 10)
                return false;

            return true;
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
    }
}